import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import {
  IUser,
  UserService,
} from '../login-and-registration/services/http/user.service';

export const dashboardResolver: ResolveFn<IUser[]> = (route, state) => {
  const userService = inject(UserService);

  return userService.getUsers();
};
