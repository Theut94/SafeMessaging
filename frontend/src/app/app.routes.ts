import { Routes } from '@angular/router';
import LoginComponent from './pages/login-and-registration/login/login.component';
import RegisterComponent from './pages/login-and-registration/register/register.component';
import DashboardComponent from './pages/dashboard/dashboard.component';
import { dashboardGuard } from './pages/dashboard/dashboard.guard';
import { dashboardResolver } from './pages/dashboard/dashboard.resolver';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [dashboardGuard],
    resolve: { dashboardData: dashboardResolver },
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }, // Default
  { path: '**', redirectTo: '/login' },
];
