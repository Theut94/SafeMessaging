import { Injectable } from '@angular/core';
import { EncryptionService } from '../security/encryption.service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl;

  // For logging in and registering
  constructor(
    private encryptionService: EncryptionService,
    private http: HttpClient
  ) {}

  register(username: string, password: string, salt: string) {
    return this.http.post(this.apiUrl + '/register', {
      username,
      password,
      salt,
    });
  }
}
