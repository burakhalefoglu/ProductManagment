import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Language } from '../models/language';
import {environment} from '../../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  constructor(private readonly _httpClient: HttpClient) { }

  getLanguageList(): Observable<Language[]> {
    return this._httpClient.get<Language[]>(environment.getApiUrl + '/languages/');
  }

    getLanguage(id: number | undefined): Observable<Language> {
    return this._httpClient.get<Language>(environment.getApiUrl + `/languages/${id}`);
  }

  addLanguage(language: Language): Observable<any> {
    return this._httpClient.post(environment.getApiUrl + '/languages/', language, { responseType: 'text' });
  }

  updateLanguage(language: Language): Observable<any> {
    return this._httpClient.put(environment.getApiUrl + '/languages/', language, { responseType: 'text' });
  }

    deleteLanguage(id: number | undefined) {
    return this._httpClient.delete(environment.getApiUrl + `/languages/${id}`);
  }
}
