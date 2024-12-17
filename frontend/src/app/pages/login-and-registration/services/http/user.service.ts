import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.apiUrl + '/user/GetAllUsersAsync', {});
  }
}

export interface IUser {
  firstName: string;
  lastName: string;
  guid: string;
  publicKey: string;
}
