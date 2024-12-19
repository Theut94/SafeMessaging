import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

export const dashboardGuard: CanActivateFn = (route, state) => {
  const snackBar = inject(MatSnackBar);
  const router = inject(Router);

  const token = sessionStorage.getItem('token') || '';

  // Check if the token exists - the backend has auth and verifies that the token is the correct one
  // so even if the user generates a "random" token, it will not be valid, it would show them the dashboard route, but just an empty dashboard with no data.
  if (!token) {
    router.navigate(['/login']);
    snackBar.open('You do not have a valid token.', ' close', {
      duration: 5 * 1000,
    });
    return false;
  }

  const decodedToken = JSON.parse(atob(token.split('.')[1]));
  const userId = decodedToken.sub; //extract the user ID
  sessionStorage.setItem('user', userId);

  return true;
};
