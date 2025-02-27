import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EncryptionService } from '../services/security/encryption.service';
import { AuthService } from '../services/http/auth.service';
import { ILoginUserDTO } from '../services/http/auth.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export default class LoginComponent implements OnDestroy {
  destroy$ = new Subject<void>();
  email: string = '';
  password: string = '';

  loginErrorText = '';
  loading = false;

  constructor(
    private router: Router,
    private encryptionService: EncryptionService,
    private authService: AuthService
  ) {}

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  async onSubmit() {
    this.loading = true;

    const salt = localStorage.getItem('salt') || '';

    const hashedPassword = await this.encryptionService.hashPassword(
      this.password,
      salt
    );

    sessionStorage.setItem('hashedPassword', hashedPassword);

    const loginUserDTO: ILoginUserDTO = {
      username: this.email,
      password: hashedPassword,
      salt: salt,
    };

    this.authService
      .login(loginUserDTO)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (response) => {
          this.loading = false;
          this.loginErrorText = '';
          sessionStorage.setItem('token', response.token);
          this.router.navigate(['/dashboard']);
          return response;
        },
        (error) => {
          this.loading = false;
          sessionStorage.clear();
          console.error('Login error:', error);
          this.loginErrorText = 'Login failed. Please try again.';
        }
      );

    this.loading = false;
  }
}
