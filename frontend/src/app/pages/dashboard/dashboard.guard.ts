import { CanActivateFn } from '@angular/router';

export const dashboardGuard: CanActivateFn = (route, state) => {
  // TODO: Implement JWT guard
  return true;
};
