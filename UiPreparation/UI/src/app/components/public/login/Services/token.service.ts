import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import {LocalStorageService} from '../../../../core/services/local-storage.service';
import {environment} from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

constructor(private httpClient: HttpClient, private storageService: LocalStorageService) { }

refreshToken(): Observable<any> | null {
  if (this.storageService.getItem('refreshToken') !== null) {
    return this.httpClient
      .post<any>(environment.getApiUrl + '/Auth/refresh-token', {refreshToken: this.storageService.getItem('refreshToken')})
      .pipe(tap(res => {
        if (res.success) {
          this.storageService.setToken(res.data.token);
          this.storageService.setItem('refreshToken', res.data.refreshToken);
        }
      }));
  }
  return null;
}

}
