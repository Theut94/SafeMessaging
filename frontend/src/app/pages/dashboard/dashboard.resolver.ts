import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import {
  IUser,
  UserService,
} from '../login-and-registration/services/http/user.service';
import { of, Subject, takeUntil } from 'rxjs';

export const dashboardResolver: ResolveFn<IUser[]> = (route, state) => {
  const userService = inject(UserService);
  // const destroy$ = new Subject<void>();

  return of([
    {
      firstName: 'John',
      lastName: 'Doe',
      guid: 'b3d5f5e2-1234-4a9b-bb0d-fdf8e4730a2f',
      publicKey:
        'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA7j2jsdklfkjsdlfjsldkjflkjsdlfjsdf==',
    },
    {
      firstName: 'Jane',
      lastName: 'Smith',
      guid: '4f1b6a7c-9c82-492b-bd7f-df6a2fe5f55f',
      publicKey:
        'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA6d9gfp9ldfjsl98jsdklfjsdlkfjdsl==',
    },
    {
      firstName: 'Alice',
      lastName: 'Johnson',
      guid: '3a9e89a8-25db-44d1-bb9f-63a85ecff7a2',
      publicKey:
        'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA8d8gslfjsdlfjsd9jsl9djs0djldjf==',
    },
  ]);

  return userService.getUsers();

  // .pipe(takeUntil(destroy$))
  // .subscribe(
  //   (response) => {
  //     return response;
  //   },
  //   (error) => {
  //     console.error('Failed to load users', error);
  //   }
  // );

  // return users;
};
