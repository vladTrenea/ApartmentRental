import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/internal/Observable';
import {map} from 'rxjs/operators';

import {AuthApiService} from './auth-api.service';
import {LoginModel} from '../models/login.model';
import {UserAuthorization} from '../models/user-authorization.model';
import {StorageService} from './storage.service';
import {RegisterModel} from '../models/register.model';
import {ResetPasswordModel} from '../models/reset-password.model';
import {ChangePasswordModel} from '../models/change-password.model';
import {AccountModel} from '../../core/master-page/pages/account/models/account.model';

@Injectable()
export class AuthService {

    constructor(private authApiService: AuthApiService, private storageService: StorageService) {

    }

    login(loginModel: LoginModel): Observable<UserAuthorization> {
        return this.authApiService.login(loginModel).pipe(
            map((response: UserAuthorization) => {
                this.storageService.setUserAuthorization(response);

                return response;
            }));
    }

    register(registerModel: RegisterModel): Observable<any> {
        return this.authApiService.register(registerModel);
    }

    confirmAccount(emailToken: string): Observable<any> {
        return this.authApiService.confirmAccount(emailToken);
    }

    resetPassword(resetPasswordModel: ResetPasswordModel): Observable<any> {
        return this.authApiService.resetPassword(resetPasswordModel);
    }

    getResetPasswordTokenValidity(resetPasswordToken: string): Observable<any> {
        return this.authApiService.getResetPasswordTokenValidity(resetPasswordToken);
    }

    changePassword(resetPasswordToken: string, changePasswordModel: ChangePasswordModel): Observable<any> {
        return this.authApiService.changePassword(resetPasswordToken, changePasswordModel);
    }

    logout(): void {
        this.storageService.removeUserAuthorization();
    }

    updateAccount(account: AccountModel): Observable<any> {
        return this.authApiService.updateAccount(account).pipe(
            (map(() => {
                const userAuth = this.getCurrentUserAuthorization();
                userAuth.lastName = account.lastName;
                userAuth.firstName = account.firstName;
                this.storageService.setUserAuthorization(userAuth);
            }))
        );
    }

    getCurrentUserAuthorization(): UserAuthorization {
        return this.storageService.getUserAuthorization();
    }
}
