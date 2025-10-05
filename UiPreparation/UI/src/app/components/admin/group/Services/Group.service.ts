import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Group} from '../Models/Group';
import {environment} from '../../../../../environments/environment';
import {LookUp} from '../../../../core/models/LookUp';

@Injectable({
  providedIn: 'root',
})
export class GroupService {
  constructor(private readonly _httpClient: HttpClient) {}

  getGroupList(): Observable<Group[]> {
    return this._httpClient.get<Group[]>(environment.getApiUrl + '/groups/');
  }

  addGroup(group: Group): Observable<any> {
      return this._httpClient.post(
        environment.getApiUrl + '/groups/',
        group,
        {responseType: 'text'}
    );
  }

  updateGroup(group: Group): Observable<any> {
      return this._httpClient.put(
        environment.getApiUrl + '/groups/',
        group,
        {responseType: 'text'}
    );
  }
  deleteGroup(id: number) {
    return this._httpClient.request(
      'delete',
      environment.getApiUrl + `/groups/${id}`
    );
  }
  getGroupById(id: number): Observable<Group> {
    return this._httpClient.get<Group>(environment.getApiUrl + `/groups/${id}`);
  }

  getGroupClaims(id: number): Observable<LookUp[]> {
    return this._httpClient.get<LookUp[]>(
      environment.getApiUrl + `/group-claims/groups/${id}`
    );
  }

  saveGroupClaims(groupId: number, claims: number[]): Observable<any> {
      return this._httpClient.put(
        environment.getApiUrl + `/group-claims/`,
        {GroupId: groupId, ClaimIds: claims},
        {responseType: 'text'}
    );
  }

  getGroupUsers(id: number): Observable<LookUp[]> {
    return this._httpClient.get<LookUp[]>(
      environment.getApiUrl + `/user-groups/groups/${id}/users`
    );
  }

  saveGroupUsers(groupId: number, userIds: number[]): Observable<any> {
      return this._httpClient.put(
        environment.getApiUrl + '/user-groups/groups/',
        {GroupId: groupId, UserIds: userIds},
        {responseType: 'text'}
    );
  }
}
