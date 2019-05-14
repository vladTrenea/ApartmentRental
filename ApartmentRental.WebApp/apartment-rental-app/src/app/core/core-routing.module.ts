import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {MasterPageComponent} from './master-page/master-page.component';
import {LoginComponent} from './login/login.component';
import {AuthGuard} from '../shared/guards/auth.guard';
import {AdminGuard} from '../shared/guards/admin.guard';
import {RegisterComponent} from './register/register.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import {ChangePasswordComponent} from './change-password/change-password.component';
import {ConfirmAccountComponent} from './confirm-account/confirm-account.component';

const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'confirm-account/:token',
        component: ConfirmAccountComponent
    },
    {
        path: 'forgot-password',
        component: ForgotPasswordComponent
    },
    {
        path: 'change-password/:token',
        component: ChangePasswordComponent
    },
    {
        path: '',
        component: MasterPageComponent,
        children: [
            {
                path: 'apartments',
                loadChildren: 'app/core/master-page/pages/apartments/apartments.module#ApartmentsModule',
                canActivate: [AuthGuard]
            },
            {
                path: 'users',
                loadChildren: 'app/core/master-page/pages/users/users.module#UsersModule',
                canActivate: [AdminGuard]
            },
            {
                path: 'account',
                loadChildren: 'app/core/master-page/pages/account/account.module#AccountModule',
                canActivate: [AuthGuard]
            },
            {
                path: '',
                redirectTo: '/login',
                pathMatch: 'full'
            }
        ]
    },
    {
        path: '',
        loadChildren: 'app/error/error.module#ErrorModule'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class CoreRoutingModule {

}
