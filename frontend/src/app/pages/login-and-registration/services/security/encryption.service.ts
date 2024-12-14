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

  encryptString(iv: string, hash: string, stringToBeEncrypted: string): string {
    // Create a key using the hash
    const key = CryptoJS.enc.Hex.parse(hash); // Convert hash string to WordArray
    const ivWordArray = CryptoJS.enc.Hex.parse(iv); // Convert IV from hex string to WordArray

    // Encrypt the data
    const encrypted = CryptoJS.AES.encrypt(stringToBeEncrypted, key, {
      iv: ivWordArray,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    return encrypted.toString(); // Returns the encrypted data as a string
  }

  // Decrypt Function
  decryptString(iv: string, hash: string, encryptedString: string): string {
    // Create a key using the hash
    const key = CryptoJS.enc.Hex.parse(hash); // Convert hash string to WordArray
    const ivWordArray = CryptoJS.enc.Hex.parse(iv); // Convert IV from hex string to WordArray

    // Decrypt the data
    const decrypted = CryptoJS.AES.decrypt(encryptedString, key, {
      iv: ivWordArray,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    // Convert decrypted data back to string
    return decrypted.toString(CryptoJS.enc.Utf8); // Returns the decrypted data as a string
  }
}
