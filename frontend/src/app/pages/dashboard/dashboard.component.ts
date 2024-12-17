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
  users: IUser[] = [];
  selectedUser!: IUser;
  messages: { sender: string; text: string }[] = [];
  newMessage = '';

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    // Access the resolved data from the route
    this.route.data.subscribe((data) => {
      this.users = data['dashboardData'];
      if (!this.users || this.users.length === 0) {
        console.log('No users found.');
      }
    });
  }

  selectUser(user: any) {
    this.selectedUser = user;
    this.messages = [
      { sender: 'me', text: 'Hello!' },
      { sender: user.firstName, text: 'Hi there!' },
    ];
  }

  sendMessage() {
    if (this.newMessage.trim()) {
      this.messages.push({ sender: 'me', text: this.newMessage });
      this.newMessage = '';
    }
  }
}
