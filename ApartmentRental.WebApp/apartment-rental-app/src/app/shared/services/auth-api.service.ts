import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/internal/Observable';

import {ApiService} from './api.service';
import {LoginModel} from '../models/login.model';
import {UserAuthorization} from '../models/user-authorization.model';
import {StorageService} from './storage.service';
import {RegisterModel} from '../models/register.model';
import {ResetPasswordModel} from '../models/reset-password.model';
import {ChangePasswordModel} from '../models/change-password.model';
import {AccountModel} from '../../core/master-page/pages/account/models/account.model';

@Injectable()
export class AuthApiService extends ApiService {

    constructor(httpClient: HttpClient, storageService: StorageService) {
        super(httpClient, storageService);
    }

    login(login: LoginModel): Observable<UserAuthorization> {
        return this.httpClient.post<UserAuthorization>(this.endpoints.login, JSON.stringify(login),
            {headers: this.getRequestHeaders()});
    }

    register(register: RegisterModel): Observable<any> {
        return this.httpClient.post(this.endpoints.register, JSON.stringify(register),
            {headers: this.getRequestHeaders()});
    }

    confirmAccount(emailToken: string): Observable<any> {
        return this.httpClient.post(`${this.endpoints.confirmAccount}/${emailToken}`, {},
            {headers: this.getRequestHeaders()});
    }

    resetPassword(resetPassword: ResetPasswordModel): Observable<any> {
        return this.httpClient.post(this.endpoints.resetPassword, JSON.stringify(resetPassword),
            {headers: this.getRequestHeaders()});
    }

    getResetPasswordTokenValidity(resetPasswordToken: string) {
        return this.httpClient.post(`${this.endpoints.passwordTokenValidity}/${resetPasswordToken}`, {},
            {headers: this.getRequestHeaders()});
    }

    changePassword(resetPasswordToken: string, changePasswordModel: ChangePasswordModel): Observable<any> {
        return this.httpClient.put(`${this.endpoints.changePassword}/${resetPasswordToken}`, JSON.stringify(changePasswordModel),
            {headers: this.getRequestHeaders()});
    }

    updateAccount(account: AccountModel): Observable<any> {
        return this.httpClient.put(`${this.endpoints.account}`, JSON.stringify(account),
            {headers: this.getRequestHeaders()});
    }
}
