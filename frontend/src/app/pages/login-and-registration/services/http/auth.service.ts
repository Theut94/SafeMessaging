import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  register(registerUserDTO: IRegisterUserDTO) {
    return this.http.post(this.apiUrl + '/auth/register', {
      ...registerUserDTO,
    });
  }

  login(LoginUserDTO: ILoginUserDTO): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(this.apiUrl + '/auth/login', {
      LoginUserDTO,
    });
  }
}

export interface IRegisterUserDTO {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
  publicKey: string;
  salt: string;
}

export interface ILoginUserDTO {
  username: string;
  password: string;
  salt: string;
}
