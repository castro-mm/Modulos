import { Routes } from '@angular/router';
import { LoginComponent } from './components/login.component';
import { RegisterComponent } from './components/register.component';
import { ForgotPasswordComponent } from './components/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password.component';
import { ConfirmEmailComponent } from './components/confirm-email.component';
import { ChangePasswordComponent } from './components/change-password.component';
import { authGuard } from './guards/auth.guard';

export default [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'forgot-password', component: ForgotPasswordComponent },
    { path: 'reset-password', component: ResetPasswordComponent },
    { path: 'confirm-email', component: ConfirmEmailComponent },
    { path: 'change-password', component: ChangePasswordComponent, canActivate: [authGuard] },
] as Routes;
