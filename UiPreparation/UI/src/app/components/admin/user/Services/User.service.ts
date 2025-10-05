import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {User} from '../models/User';
import {PasswordDto} from '../models/passwordDto';
import {environment} from '../../../../../environments/environment';
import {LookUp} from '../../../../core/models/LookUp';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  getUserList(): Observable<User[]> {

    return this.httpClient.get<User[]>(environment.getApiUrl + '/users/');

  }

    getUserById(id: number | undefined): Observable<User> {

    return this.httpClient.get<User>(environment.getApiUrl + `/users/${id}`);
  }


  addUser(user: User): Observable<any> {
    return this.httpClient.post(environment.getApiUrl + '/users/', user, {responseType: 'text'});
  }

  updateUser(user: User): Observable<any> {
      return this.httpClient.put(environment.getApiUrl + '/users/', user, {responseType: 'text'});
  }

    deleteUser(id: number | undefined) {
    return this.httpClient.request('delete', environment.getApiUrl + `/users/${id}`);
  }


    getUserClaims(userId: number | undefined) {
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + `/user-claims/users/${userId}`);
  }

    saveUserClaims(userId: number | undefined, claims: number[]): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '' +
        '/user-claims/', {UserId: userId, ClaimIds: claims}, {responseType: 'text'});
  }

    getUserGroupPermissions(userId: number | undefined): Observable<LookUp[]> {
    return this.httpClient.get<LookUp[]>(environment.getApiUrl + `/user-groups/users/${userId}/groups`);
  }

    saveUserGroupPermissions(userId: number | undefined, groups: number[]): Observable<any> {
        return this.httpClient.put(environment.getApiUrl + '/user-groups/', {UserId: userId, GroupId: groups}, {responseType: 'text'});

  }

  saveUserPassword(command: PasswordDto): Observable<any> {
      return this.httpClient.put(environment.getApiUrl + '/Auth/user-password', command, {responseType: 'text'});
  }

}
