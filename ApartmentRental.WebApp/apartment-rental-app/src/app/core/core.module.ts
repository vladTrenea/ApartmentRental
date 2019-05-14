import {RouterModule} from '@angular/router';
import {NgModule} from '@angular/core';

import {MasterPageComponent} from './master-page/master-page.component';
import {SharedModule} from '../shared/shared.module';
import {CoreRoutingModule} from './core-routing.module';
import {MenuComponent} from './master-page/menu/menu.component';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import {ChangePasswordComponent} from './change-password/change-password.component';
import {ConfirmAccountComponent} from './confirm-account/confirm-account.component';

@NgModule({
    imports: [
        SharedModule,
        CoreRoutingModule
    ],
    declarations: [
        MasterPageComponent,
        MenuComponent,
        LoginComponent,
        RegisterComponent,
        ForgotPasswordComponent,
        ChangePasswordComponent,
        ConfirmAccountComponent
    ],
    exports: [
        RouterModule
    ],
    entryComponents: [],
    providers: []
})
export class CoreModule {
}
