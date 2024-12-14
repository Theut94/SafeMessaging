import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class KeyService {
  // For Diffie-hellman stuff
  constructor() {}

  async generateKeyPair(): Promise<CryptoKeyPair> {
    const algorithm = {
      name: 'ECDH', // Elliptic Curve Diffie-Hellman
      namedCurve: 'P-256', // Curve used for the key exchange
    };

    const keyPair = await window.crypto.subtle.generateKey(algorithm, true, [
      'deriveBits',
    ]);
    return keyPair;
  }

  async generateSharedSecret(
    privateKey: CryptoKey,
    otherPublicKey: CryptoKey
  ): Promise<ArrayBuffer> {
    // const algorithm = {
    //   name: 'ECDH',
    //   namedCurve: 'P-256',
    // };

    const sharedSecret = await window.crypto.subtle.deriveBits(
      {
        name: 'ECDH',
        public: otherPublicKey,
      },
      privateKey,
      256 // Length in bits (32 bytes = 256 bits)
    );

    return sharedSecret;
  }

  async exportPublicKey(publicKey: CryptoKey): Promise<string> {
    const exportedKey = await window.crypto.subtle.exportKey('spki', publicKey);
    return this.arrayBufferToBase64(exportedKey);
  }

  async importPublicKey(base64PublicKey: string): Promise<CryptoKey> {
    const publicKeyBuffer = this.base64ToArrayBuffer(base64PublicKey);

    return window.crypto.subtle.importKey(
      'spki',
      publicKeyBuffer,
      { name: 'ECDH', namedCurve: 'P-256' },
      true,
      []
    );
  }

  async exportPrivateKey(privateKey: CryptoKey): Promise<string> {
    const exportedKey = await window.crypto.subtle.exportKey(
      'pkcs8',
      privateKey
    );

    return this.arrayBufferToBase64(exportedKey);
  }

  async importPrivateKey(base64PrivateKey: string): Promise<CryptoKey> {
    const privateKeyBuffer = this.base64ToArrayBuffer(base64PrivateKey);

    return window.crypto.subtle.importKey(
      'pkcs8',
      privateKeyBuffer,
      { name: 'ECDH', namedCurve: 'P-256' },
      true,
      ['deriveBits']
    );
  }

  arrayBufferToBase64(buffer: ArrayBuffer): string {
    const uint8Array = new Uint8Array(buffer);
    let binary = '';
    uint8Array.forEach((byte) => {
      binary += String.fromCharCode(byte);
    });
    return btoa(binary);
  }

  private base64ToArrayBuffer(base64: string): ArrayBuffer {
    const binaryString = atob(base64);
    const length = binaryString.length;
    const arrayBuffer = new ArrayBuffer(length);
    const uint8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < length; i++) {
      uint8Array[i] = binaryString.charCodeAt(i);
    }
    return arrayBuffer;
  }
}
