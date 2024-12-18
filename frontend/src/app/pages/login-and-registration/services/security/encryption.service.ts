import { Injectable } from '@angular/core';
import * as scrypt from 'scrypt-js';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root',
})
export class EncryptionService {
  //For encryption, decryption and hashing
  constructor() {}

  generateSalt(length: number = 16): string {
    const array = new Uint8Array(length);
    window.crypto.getRandomValues(array);
    return Array.from(array, (byte) => byte.toString(16).padStart(2, '0')).join(
      ''
    );
  }

  async hashPassword(password: string, salt: string): Promise<string> {
    const encoder = new TextEncoder();
    const passwordBuffer = encoder.encode(password);
    const saltBuffer = encoder.encode(salt);

    const N = 16384; // CPU/memory cost
    const r = 8; // Block size
    const p = 1; // Parallelization

    try {
      const keyBuffer = await scrypt.scrypt(
        passwordBuffer,
        saltBuffer,
        N,
        r,
        p,
        32 // Length of the generated hash in bytes (64 in hex)
      );

      // Convert the resulting ArrayBuffer to a hex string
      return this.bufferToHex(keyBuffer);
    } catch (error) {
      console.error('Error hashing password:', error);
      throw new Error('Hashing failed');
    }
  }

  private bufferToHex(buffer: ArrayBuffer): string {
    return Array.from(new Uint8Array(buffer))
      .map((byte) => byte.toString(16).padStart(2, '0'))
      .join('');
  }

  generateIV(): string {
    return window.crypto.getRandomValues(new Uint8Array(16)).toString();
  }

  encryptKey(iv: string, hash: string, stringToBeEncrypted: string): string {
    // Parse input because we're not interested in storing word arrays.
    const key = CryptoJS.enc.Hex.parse(hash);
    const ivWordArray = CryptoJS.enc.Hex.parse(iv);

    const encrypted = CryptoJS.AES.encrypt(stringToBeEncrypted, key, {
      iv: ivWordArray,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    return encrypted.toString();
  }

  decryptKey(iv: string, hash: string, encryptedString: string): string {
    const key = CryptoJS.enc.Hex.parse(hash);
    const ivWordArray = CryptoJS.enc.Hex.parse(iv);

    // Decrypt the data
    const decrypted = CryptoJS.AES.decrypt(encryptedString, key, {
      iv: ivWordArray,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    // Convert decrypted data back to string
    return decrypted.toString(CryptoJS.enc.Utf8);
  }

  // We use different functions for encrypting keys and messages because the shared secret (key) is in a different format.
  encryptMessage(
    iv: string,
    sharedSecret: string,
    stringToBeEncrypted: string
  ): string {
    // Create a key using the sharedSecret with base 64 in this case because that is what we we change the shared secret to
    const key = CryptoJS.enc.Base64.parse(sharedSecret);
    const ivWordArray = CryptoJS.enc.Hex.parse(iv);

    const encrypted = CryptoJS.AES.encrypt(stringToBeEncrypted, key, {
      iv: ivWordArray,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    return encrypted.toString();
  }

  decryptMessage(
    iv: string,
    sharedSecret: string,
    encryptedString: string
  ): string {
    // Create a key using the sharedSecret with base 64 in this case because that is what we we change the shared secret to
    const key = CryptoJS.enc.Base64.parse(sharedSecret);
    const ivWordArray = CryptoJS.enc.Hex.parse(iv);

    // Decrypt the data
    const decrypted = CryptoJS.AES.decrypt(encryptedString, key, {
      iv: ivWordArray,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    // Convert decrypted data back to string
    return decrypted.toString(CryptoJS.enc.Utf8);
  }
}
