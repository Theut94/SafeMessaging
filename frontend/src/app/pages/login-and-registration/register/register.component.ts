import { Component, inject, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subject, takeUntil } from 'rxjs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { AuthService } from '../services/http/auth.service';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EncryptionService } from '../services/security/encryption.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    RouterModule,
    CommonModule,
    MatCheckboxModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './register.html',
  styleUrls: ['./register.scss'],
})
export default class RegisterComponent implements OnDestroy {
  destroy$ = new Subject<void>();
  private _snackBar = inject(MatSnackBar);

  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  registrationErrorText = '';

  loading: boolean = false;

  passwordStrength: string = 'Weak';
  strengthLevel: number = 0;

  lengthCriteria = false;
  numberCriteria = false;
  uppercaseCriteria = false;
  lowercaseCriteria = false;
  specialCharCriteria = false;

  showPasswordTips = true;

  constructor(
    private router: Router,
    private authService: AuthService,
    private encryptionService: EncryptionService
  ) {}

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  checkPasswordStrength(password: string): void {
    this.registrationErrorText = '';
    const lengthCriteria = password.length >= 8; // Minimum length
    const numberCriteria = /\d/.test(password); // At least one number
    const uppercaseCriteria = /[A-Z]/.test(password); // At least one uppercase letter
    const lowercaseCriteria = /[a-z]/.test(password); // At least one lowercase letter
    const specialCharCriteria = /[!@#$/%^&*]/.test(password); // At least one special character

    this.lengthCriteria = lengthCriteria;
    this.numberCriteria = numberCriteria;
    this.uppercaseCriteria = uppercaseCriteria;
    this.lowercaseCriteria = lowercaseCriteria;
    this.specialCharCriteria = specialCharCriteria;

    this.strengthLevel = 0; // Reset strength level

    // Increment strength level based on criteria
    if (lengthCriteria) this.strengthLevel++;
    if (numberCriteria) this.strengthLevel++;
    if (uppercaseCriteria) this.strengthLevel++;
    if (lowercaseCriteria) this.strengthLevel++;
    if (specialCharCriteria) this.strengthLevel++;

    // Set password strength based on level
    switch (this.strengthLevel) {
      case 0:
      case 1:
        this.passwordStrength = 'Weak';
        break;
      case 2:
        this.passwordStrength = 'Moderate';
        break;
      case 3:
      case 4:
        this.passwordStrength = 'Strong';
        break;
      case 5:
        this.passwordStrength = 'Very Strong';
        break;
      default:
        this.passwordStrength = '';
    }
  }

  async onSubmit() {
    this.loading = true;

    const salt = this.encryptionService.generateSalt();
    console.log('salt: ', salt);
    console.log('salt.length: ', salt.length);

    localStorage.setItem('userSalt', salt);

    const hashedPassword = await this.encryptionService.hashPassword(
      this.password,
      salt
    );

    console.log('hashedPassword: ', hashedPassword);
    console.log('hashedPassword.length: ', hashedPassword.length);

    this.authService
      .register(this.email, hashedPassword, salt)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (response) => {
          this.loading = false;
          this._snackBar.open(
            'An email have been sent to your mail, if you do not already have an account.',
            ' close',
            { duration: 5 * 1000 }
          ); // Mimic that we have email confirmation

          this.registrationErrorText = '';
          this.router.navigate(['/login']);
          return response;
        },
        (error) => {
          this.loading = false;
          console.error('Registration error:', error);
          this.registrationErrorText = 'Registration failed. Please try again.';
        }
      );
  }

  get getFormStatus(): boolean {
    return (
      this.lengthCriteria &&
      this.numberCriteria &&
      this.uppercaseCriteria &&
      this.lowercaseCriteria &&
      this.specialCharCriteria &&
      this.password === this.confirmPassword &&
      this.email.includes('@') &&
      this.email.includes('.')
    );
  }
}
