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

    // Setup
    const salt = this.encryptionService.generateSalt();
    const hashedPassword = await this.encryptionService.hashPassword(
      this.password,
      salt
    );
    const IV = this.encryptionService.generateIV();

    // Generate two key pairs for proof of concept
    const aliceKeyPair = await this.keyService.generateKeyPair();
    const alicePublicKey = aliceKeyPair.publicKey;
    const alicePrivateKey = aliceKeyPair.privateKey;

    const bobKeyPair = await this.keyService.generateKeyPair();
    const bobPublicKey = bobKeyPair.publicKey;
    const bobPrivateKey = bobKeyPair.privateKey;

    // Export keys because we would do this before
    // 1. Sending the public key over the network
    // 2. Encrypting the private key and storing it locally
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

    // Import the keys because we would do this before we can make a shared secret with:
    // Our own decrypted private key from local storage
    // Another user's public key
    const aliceImportedPrivateKey = await this.keyService.importPrivateKey(
      aliceBase64PrivateKey
    );

    const aliceImportedPublicKey = await this.keyService.importPublicKey(
      aliceBase64PublicKey
    );

    const bobImportedPrivateKey = await this.keyService.importPrivateKey(
      bobBase64PrivateKey
    );

    const bobImportedPublicKey = await this.keyService.importPublicKey(
      bobBase64PublicKey
    );

    // Generate shared secrets with the combinations avaiable with two key pairs
    // (Both keys should be identical and have a byte length of 32)
    const sharedSecretBobPrivateAlicePublic =
      await this.keyService.generateSharedSecret(
        aliceImportedPrivateKey,
        bobImportedPublicKey
      );

    const sharedSecretAlicePrivateBobPublic =
      await this.keyService.generateSharedSecret(
        bobImportedPrivateKey,
        aliceImportedPublicKey
      );

    // Convert the shared secret (ArrayBuffer) to base 64 strings (because we use this to encrypt and decrypt)
    const base64SharedSecretBobPrivateAlicePublic =
      this.keyService.arrayBufferToBase64(sharedSecretBobPrivateAlicePublic);

    const base64SharedSecretAlicePrivateBobPublic =
      this.keyService.arrayBufferToBase64(sharedSecretAlicePrivateBobPublic);

    // Test 1: Encrypt a message with the shared secrets and the message IV:
    // 1: shared secret generated with Bob private key and Alice public key
    const stringToBeEncrypted = 'My name Jeeeff!';
    console.log(
      'String before encryption with bob private key and alice public key: ',
      stringToBeEncrypted
    );
    const encryptedString = this.encryptionService.encryptMessage(
      IV,
      base64SharedSecretBobPrivateAlicePublic,
      stringToBeEncrypted
    );
    console.log('String after encryption: ', encryptedString);
    // Test 1: Decrypt a message with the shared secrets and the message IV:
    // 2: shared secret generated with Alice private key and Bob public key
    const decryptString = this.encryptionService.decryptMessage(
      IV,
      base64SharedSecretAlicePrivateBobPublic,
      encryptedString
    );
    console.log(
      'String after decryption with alice private and bob public: ',
      decryptString
    );

    // Test 2: Encrypt a private key as to not store it decrypted locally.
    // Here we use a hashed master password and the IV to encrypt and decrypt
    console.log(
      'Shared secret before encryption: ',
      base64SharedSecretAlicePrivateBobPublic
    );
    const encryptSharedSecret = this.encryptionService.encryptKey(
      IV,
      hashedPassword,
      base64SharedSecretAlicePrivateBobPublic
    );
    console.log('Encrypted shared secret: ', encryptSharedSecret);

    const decryptedSharedSecret = this.encryptionService.decryptKey(
      IV,
      hashedPassword,
      encryptSharedSecret
    );

    console.log('Decrypted shared secret: ', decryptedSharedSecret);
    this.loading = false;
    //this.router.navigate(['/dashboard']);
  }
}
