import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { EncryptionService } from '../services/security/encryption.service';
import { KeyService } from '../services/security/key.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterModule, CommonModule],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export default class LoginComponent {
  email: string = '';
  password: string = '';

  loginErrorText = '';
  loading = false;

  constructor(
    private router: Router,
    private encryptionService: EncryptionService,
    private keyService: KeyService
  ) {}

  async onSubmit() {
    this.loading = true;
    // this.keyService.testPublicKey();
    const salt = this.encryptionService.generateSalt();
    const hashedPassword = await this.encryptionService.hashPassword(
      this.password,
      salt
    );
    const IV = this.encryptionService.generateIV();

    const aliceKeyPair = await this.keyService.generateKeyPair();
    const alicePublicKey = aliceKeyPair.publicKey;
    const alicePrivateKey = aliceKeyPair.privateKey;

    console.log('aliceKeyPair: ', aliceKeyPair);
    const bobKeyPair = await this.keyService.generateKeyPair();
    const bobPublicKey = bobKeyPair.publicKey;
    const bobPrivateKey = bobKeyPair.privateKey;
    console.log('bobKeyPair: ', bobKeyPair);

    const aliceBase64PublicKey = await this.keyService.exportPublicKey(
      alicePublicKey
    );
    const aliceBase64PrivateKey = await this.keyService.exportPrivateKey(
      alicePrivateKey
    );
    const bobBase64PublicKey = await this.keyService.exportPublicKey(
      bobPublicKey
    );
    const bobBase64PrivateKey = await this.keyService.exportPrivateKey(
      bobPrivateKey
    );

    console.log('aliceBase64PublicKey: ', aliceBase64PublicKey);
    console.log('aliceBase64PrivateKey: ', aliceBase64PrivateKey);
    console.log('bobBase64PublicKey: ', bobBase64PublicKey);
    console.log('bobBase64PrivateKey: ', bobBase64PrivateKey);

    const aliceImportedPrivateKey = await this.keyService.importPrivateKey(
      aliceBase64PrivateKey
    );
    console.log('aliceImportedPrivateKey: ', aliceImportedPrivateKey);

    const aliceImportedPublicKey = await this.keyService.importPublicKey(
      aliceBase64PublicKey
    );
    console.log('aliceImportedPublicKey: ', aliceImportedPublicKey);

    const bobImportedPrivateKey = await this.keyService.importPrivateKey(
      bobBase64PrivateKey
    );
    console.log('bobImportedPrivateKey: ', bobImportedPrivateKey);

    const bobImportedPublicKey = await this.keyService.importPublicKey(
      bobBase64PublicKey
    );
    console.log('bobImportedPublicKey: ', bobImportedPublicKey);

    const sharedSecret = await this.keyService.generateSharedSecret(
      aliceImportedPrivateKey,
      bobImportedPublicKey
    );

    console.log('sharedSecret: ', sharedSecret);
    console.log('sharedSecret.byteLength: ', sharedSecret.byteLength);

    const base64SharedSecret =
      this.keyService.arrayBufferToBase64(sharedSecret);

    console.log('base64SharedSecret: ', base64SharedSecret);

    const stringToBeEncrypted = 'My name Jeeeff!';
    console.log('String before encryption: ', stringToBeEncrypted);
    const encryptedString = this.encryptionService.encryptString(
      IV,
      hashedPassword,
      stringToBeEncrypted
    );
    console.log('String after encryption: ', encryptedString);
    const decryptString = this.encryptionService.decryptString(
      IV,
      hashedPassword,
      encryptedString
    );
    console.log('String after decryption: ', decryptString);
    console.log('Login pressed!');
    this.loading = false;
    //this.router.navigate(['/dashboard']);
  }
}
