import { CanActivateFn, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

export const dashboardGuard: CanActivateFn = (route, state) => {
  return true;
  const snackBar = inject(MatSnackBar);
  const router = inject(Router); // Router for redirection
  const jwtHelper = new JwtHelperService();

  const token = localStorage.getItem('token') || '';
  const decodedToken = jwtHelper.decodeToken(token);

  // Check if the token exists
  if (!decodedToken) {
    router.navigate(['/login']);
    snackBar.open('You do not have a valid token.', ' close', {
      duration: 5 * 1000,
    }); // Mimic that we have email confirmation
    return false;
  }

  // Check if the token is expired
  if (jwtHelper.isTokenExpired(decodedToken)) {
    router.navigate(['/login']);
    snackBar.open('You do not have a valid token.', ' close', {
      duration: 5 * 1000,
    }); // Mimic that we have email confirmation
    return false;
  }

  return true; // Token is valid, allow access
};
