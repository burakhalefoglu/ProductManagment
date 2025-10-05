import { Injectable, DestroyRef, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {environment} from '../../../environments/environment';


@Injectable({
    providedIn: 'root'
})
export class HttpEntityRepositoryService<T> {

    private httpClient = inject(HttpClient);

    constructor() { }

    getAll(_url: string, destroyRef?: DestroyRef): Observable<T[]> {
        const request$ = this.httpClient.get<T[]>(environment.getApiUrl + _url);
        return destroyRef
            ? request$.pipe(takeUntilDestroyed(destroyRef))
            : request$;
    }

    get(_url: string, id?: number, destroyRef?: DestroyRef): Observable<T> {
        const idParam = (id !== undefined && id !== null) ? +id : '';
        const request$ = this.httpClient.get<T>(environment.getApiUrl + _url + idParam
    );
        return destroyRef
            ? request$.pipe(takeUntilDestroyed(destroyRef))
            : request$;
    }

    add(_url: string, _content: any, destroyRef?: DestroyRef): Observable<T> {
        const request$ = this.httpClient.post<T>(environment.getApiUrl + _url, _content, { responseType: 'text' as 'json' }
        );

        return destroyRef
            ? request$.pipe(takeUntilDestroyed(destroyRef))
            : request$;
    }

    update(_url: string, _content: any, destroyRef?: DestroyRef): Observable<T> {
        const request$ = this.httpClient.put<T>(environment.getApiUrl + _url, _content, { responseType: 'text' as 'json' });

        return destroyRef
            ? request$.pipe(takeUntilDestroyed(destroyRef))
            : request$;
    }

    delete(_url: string, id: number, destroyRef?: DestroyRef): Observable<T> {
        const request$ = this.httpClient.delete<T>(environment.getApiUrl + _url + id, { responseType: 'text' as 'json' });

        return destroyRef
            ? request$.pipe(takeUntilDestroyed(destroyRef))
            : request$;
    }
}
