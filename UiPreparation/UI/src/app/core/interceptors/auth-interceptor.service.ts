import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import {catchError, filter, finalize, switchMap, take} from 'rxjs/operators';
import {TokenService} from '../../components/public/login/Services/token.service';
import {AlertifyService} from '../services/Alertify.service';
import {Router} from '@angular/router';
import {AuthService} from '../../components/public/login/Services/Auth.service';
import {LocalStorageService} from '../services/local-storage.service';


@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor(private tokenService: TokenService,
              private storageService: LocalStorageService,
              private alertifyService: AlertifyService,
              private router: Router) {

  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    req = this.addToken(req);

    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(req, next);
        }else if (error.status === 403) {
            return this.handle403Error();
        } else if (error.status === 500) {
            return this.handle500Error();
        }  else {
          return throwError(error);
        }
      })
    )
  }
  private addToken(req: HttpRequest<any>) {
    const lang = localStorage.getItem('lang') || 'tr-TR'

    // TODO: Refactoring needed

     return req.clone({
       setHeaders: {
         'Accept-Language': lang,
         'Authorization': `Bearer ${localStorage.getItem('token')}`

       },

       responseType: req.method === 'DELETE' ? 'text' : req.responseType
     });


  }

    private handle401Error(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (!this.isRefreshing)  {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);

            return (this.tokenService?.refreshToken?.() ?? throwError(() => new Error('TokenService is undefined'))).pipe(
                switchMap((res: any) => {
                    const newToken = res?.data?.token;
                    if (!newToken) {
                        this.handleAuthFailure('Oturum süreniz doldu. Lütfen tekrar giriş yapın.');
                        return throwError(() => new Error('Refresh returned no token'));
                    }

                    this.isRefreshing = false;
                    this.refreshTokenSubject.next(newToken);
                    return next.handle(this.addToken(req));
                }),
                catchError(err => {
                    this.handleAuthFailure('Oturum süreniz doldu. Lütfen tekrar giriş yapın.');
                    return throwError(() => err);
                }),
                finalize(() => {
                    this.isRefreshing = false;
                })
            );

        } else {
            return this.refreshTokenSubject.pipe(
                filter((token): token is string => !!token),
                take(1),
                switchMap(jwt => next.handle(this.addToken(req))),
                catchError(err => {
                    this.handleAuthFailure('Oturum süreniz doldu. Lütfen tekrar giriş yapın.');
                    return throwError(() => err);
                })
            );
        }
    }

    private handle403Error(): Observable<never> {
        this.alertifyService.warning('Yetkiniz bulunmamaktadır.');
        this.router.navigate(['/']);
        return throwError(() => new Error('403 - Forbidden'));
    }

    private handle500Error(): Observable<never> {
        this.alertifyService.error('Sunucuda bir hata oluştu. Lütfen daha sonra tekrar deneyin.');
        return throwError(() => new Error('500 - Internal Server Error'));
    }

    private handleAuthFailure(message: string): void {
        try {
            this.storageService.removeToken();
            this.storageService.removeItem('lang')
            this.storageService.removeItem('refreshToken');
        } catch {}
        this.refreshTokenSubject.next(null);
        this.alertifyService?.warning?.(message);
        this.router.navigate(['/public/login'], { queryParams: { returnUrl: this.router.url }});
    }
}
