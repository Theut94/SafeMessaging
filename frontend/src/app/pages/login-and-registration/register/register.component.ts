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
import { KeyService } from '../services/security/key.service';
import { IRegisterUserDTO } from '../services/http/auth.service';

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

  firstName: string = '';
  lastName: string = '';
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

  showPasswordTips = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private encryptionService: EncryptionService,
    private keyServicve: KeyService
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

    // we generate a salt to make sure the hashed password is unique from other passwords that are the exact same.
    const salt = this.encryptionService.generateSalt();
    const hashedPassword = await this.encryptionService.hashPassword(
      this.password,
      salt
    );
    console.log('hashedPassword: ', hashedPassword);

    const iv = this.encryptionService.generateIV();

    // this generate MY private key and MY public key. The privateKey is kept encrypted locally.
    // the public key is sent to the server so other users can generate a session key using that and THEIR private key.
    const keyPair = await this.keyServicve.generateKeyPair();

    // we export the keys to convert them to a base 64 string for easier saving.
    const privateKey = await this.keyServicve.exportPrivateKey(
      keyPair.privateKey
    );
    const publicKey = await this.keyServicve.exportPublicKey(keyPair.publicKey);

    const encryptedPrivateKey = this.encryptionService.encryptKey(
      iv,
      hashedPassword,
      privateKey
    );

    // we save the salt, to make sure the password becomes the same unique hashed value every time that specific user hashes it.
    // same for the IV for encyption when we encrypt the private key. Also we need the IV for decryption.
    // the privateKey is stored locally as nobody else will need it, this is used in DH session key generation.
    localStorage.setItem('salt', salt);
    localStorage.setItem('iv', iv);
    localStorage.setItem('privateKey', encryptedPrivateKey);

    const userDTO: IRegisterUserDTO = {
      username: this.email,
      firstName: this.firstName,
      lastName: this.lastName,
      password: hashedPassword,
      publicKey: publicKey,
      salt: salt,
    };

    this.authService
      .register(userDTO)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        (response) => {
          this.loading = false;
          this._snackBar.open(
            'An email have been sent to your mail, if you do not already have an account.',
            ' close',
            { duration: 5 * 1000 }
          );

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
      this.firstName !== '' &&
      this.lastName !== '' &&
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
