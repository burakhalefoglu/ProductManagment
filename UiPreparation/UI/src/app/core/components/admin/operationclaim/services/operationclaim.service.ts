import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../../../environments/environment';
import { OperationClaim } from '../models/operationclaim';

@Injectable({
  providedIn: 'root'
})
export class OperationClaimService {

  constructor(private readonly _httpClient: HttpClient) { }


  getOperationClaimList(): Observable<OperationClaim[]> {

    return this._httpClient.get<OperationClaim[]>(environment.getApiUrl + '/operation-claims/')
  }

  getOperationClaim(id: number): Observable<OperationClaim> {
    return this._httpClient.get<OperationClaim>(environment.getApiUrl  + `/operation-claims/${id}`)
  }

  updateOperationClaim(operationClaim: OperationClaim): Observable<any> {
    return this._httpClient.put(environment.getApiUrl  + "/operation-claims/", operationClaim, { responseType: 'text' });

  }


}