import { Component, OnInit } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IUser } from '../login-and-registration/services/http/user.service';
import { ActivatedRoute } from '@angular/router';
import {
  IChat,
  IMessage,
  MessageService,
} from '../login-and-registration/services/http/message.service';
import { Subject, takeUntil } from 'rxjs';
import { EncryptionService } from '../login-and-registration/services/security/encryption.service';
import { KeyService } from '../login-and-registration/services/security/key.service';

@Component({
  selector: 'app-dashboard',
  imports: [
    CommonModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export default class DashboardComponent implements OnInit {
  destroy$ = new Subject<void>();
  users: IUser[] = [];
  selectedUser!: IUser;
  newMessage = '';

  getChatErrorText = '';
  loading = false;
  chat!: IChat;

  constructor(
    private route: ActivatedRoute,
    private messageService: MessageService,
    private encryptionService: EncryptionService,
    private keyService: KeyService
  ) {}

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.users = data['dashboardData'];
      if (!this.users || this.users.length === 0) {
        console.log('No users found.');
      }
    });
  }

  selectUser(user: any) {
    this.selectedUser = user;
    const selectedUserGuid = this.selectedUser.guid;
    const token = sessionStorage.getItem('token') || '';

    this.messageService
      .getChat(token, selectedUserGuid)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        async (response) => {
          if (response) {
            this.chat = response;
          } else {
            return;
          }

          const encryptedPrivateKey = localStorage.getItem('privateKey') || '';
          const iv = localStorage.getItem('iv') || '';
          const hashedPassword = sessionStorage.getItem('hashedPassword') || '';

          const decryptedPrivateKey = this.encryptionService.decryptKey(
            iv,
            hashedPassword,
            encryptedPrivateKey
          );

          const privateKey = await this.keyService.importPrivateKey(
            decryptedPrivateKey
          );

          const publicKey = await this.keyService.importPublicKey(
            this.selectedUser.publicKey
          );

          const arrayBufferSharedSecret =
            await this.keyService.generateSharedSecret(privateKey, publicKey);
          const sharedsecret = await this.keyService.arrayBufferToBase64(
            arrayBufferSharedSecret
          );
          sessionStorage.setItem('sharedsecret', sharedsecret);
          if (response.messages && response.messages.length > 0) {
            this.chat.messages = response.messages.map((message) => ({
              sender: message.sender,
              text: this.encryptionService.decryptMessage(
                message.iv,
                sharedsecret,
                message.text
              ),
              GUID: message.GUID,
              iv: message.iv,
            }));
          } else {
            this.chat.messages = [];
          }
        },
        (error) => {
          //this.loading = false;
          console.error('Failed to get chat:', error);
          //this.getChatErrorText = 'No chat available. Please try again.';
        }
      );
  }

  sendMessage() {
    const sharedsecret = sessionStorage.getItem('sharedsecret') || '';

    const sender = sessionStorage.getItem('user') || '';
    const iv = this.encryptionService.generateIV();
    const message: IMessage = { iv: iv, sender: sender, text: this.newMessage };
    this.chat.messages.push(message);

    const encryptedMessage = this.encryptionService.encryptMessage(
      iv,
      sharedsecret,
      this.newMessage
    );

    const messageToSend: IMessage = {
      ...message,
      text: encryptedMessage,
    };

    this.messageService
      .sendMessage(messageToSend, this.chat.guid)
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        async (response) => {
          this.newMessage = '';
        },
        (error) => {
          console.error('Failed to send message:', error);
          this.chat.messages.pop(); // works in our simple case
        }
      );
  }
}
