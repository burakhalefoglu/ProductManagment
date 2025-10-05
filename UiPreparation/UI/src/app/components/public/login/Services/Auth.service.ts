import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {LoginUser} from '../model/login-user';
import {TokenModel} from '../model/token-model';
import {RegisterUser} from '../model/register-user';
import {environment} from '../../../../../environments/environment';
import {LocalStorageService} from '../../../../core/services/local-storage.service';
import {AlertifyService} from '../../../../core/services/Alertify.service';
import {SharedService} from '../../../../core/services/shared.service';
import {JwtHelperService} from 'angular-jwt-updated';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  userName?: string;
  jwtHelper: JwtHelperService = new JwtHelperService();
  claims?: string[];

  constructor(private httpClient: HttpClient, private storageService: LocalStorageService,
    private router: Router,
    public alertifyService: AlertifyService,
    private sharedService: SharedService) {
    this.setClaims();
  }

  login(loginUser: LoginUser) {

    let headers = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json')

    this.httpClient.post<TokenModel>(environment.getApiUrl + '/Auth/login', loginUser, { headers: headers }).subscribe(data => {


      if (data.success && data.data && data.data.token && data.data.refreshToken && data.data.claims) {

        this.storageService.setToken(data.data.token);
        this.storageService.setItem('refreshToken', data.data.refreshToken);
        this.claims = data.data.claims;

        const decode = this.jwtHelper.decodeToken(this.storageService.getToken());

        const propUserName = Object.keys(decode).filter(x => x.endsWith('/name'))[0];
        this.userName = decode[propUserName];
        this.sharedService.sendChangeUserNameEvent();

        this.router.navigateByUrl('/admin/dashboard');
      } else {
        this.alertifyService.warning(data.message || 'Login failed: Invalid response from server.');
      }

    }
    );
  }

    register(registerUser: RegisterUser) {

        let headers = new HttpHeaders();
        headers = headers.append('Content-Type', 'application/json')

        this.httpClient.post<TokenModel>(environment.getApiUrl + '/Auth/register', registerUser, { headers: headers }).subscribe(data => {
                if (data.success && data.data && data.data.token && data.data.refreshToken && data.data.claims) {
                    this.storageService.setToken(data.data.token);
                    this.storageService.setItem('refreshToken', data.data.refreshToken);
                    this.claims = data.data.claims;
                    const decode = this.jwtHelper.decodeToken(this.storageService.getToken());
                    const propUserName = Object.keys(decode).filter(x => x.endsWith('/name'))[0];
                    this.userName = decode[propUserName];
                    this.sharedService.sendChangeUserNameEvent();
                    this.router.navigateByUrl('/admin/dashboard');
                } else {
                    this.alertifyService.warning(data.message || 'Login failed: Invalid response from server.');
                }

            }
        );
    }

  getUserName(): string {
    return this.userName ?? '';
  }

  setClaims() {
    if ((this.claims === undefined || this.claims.length === 0) && this.storageService.getToken() != null && this.loggedIn() ) {
      this.httpClient.get<string[]>(environment.getApiUrl + '/operation-claims/cache').subscribe(data => {
        this.claims = data;
      })
      const token = this.storageService.getToken();
      const decode = this.jwtHelper.decodeToken(token);

      const propUserName = Object.keys(decode).filter(x => x.endsWith('/name'))[0];
      this.userName = decode[propUserName];
    }
  }

  logOut() {
    this.storageService.removeToken();
    this.storageService.removeItem('lang')
    this.storageService.removeItem('refreshToken');
    this.claims = [];
  }

  loggedIn(): boolean {
    const isExpired = this.jwtHelper.isTokenExpired(this.storageService.getToken(), -120);
    return !isExpired;
  }

  claimGuard(claim: string): boolean {
    if (!this.loggedIn()) {
        console.log('this.loggedIn(): ' + this.loggedIn());
        this.router.navigate(['/public/login']);
    }
      return (this.claims ?? []).some(function (item) {
        return item === claim;
    });
  }

}
