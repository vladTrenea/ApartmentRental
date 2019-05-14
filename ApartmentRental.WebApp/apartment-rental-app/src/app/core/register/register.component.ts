import {Component} from '@angular/core';

import {RegisterModel} from '../../shared/models/register.model';
import {AuthService} from '../../shared/services/auth.service';
import {ApiError} from '../../shared/models/api-error.model';
import {configs} from '../../shared/configs';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent {

    emailRegex = configs.validations.emailRegex;
    passwordRegex = configs.validations.passwordRegex;
    onlyLettersRegex = configs.validations.onlyLettersRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;

    registerModel: RegisterModel = new RegisterModel();
    formSubmitted = false;
    errorMessage: string;
    registered: boolean;

    constructor(private authService: AuthService) {
    }

    register(valid: boolean): void {
        this.formSubmitted = true;

        if (valid) {
            this.authService.register(this.registerModel).subscribe(
                () => {
                    this.registered = true;
                },
                (error: ApiError) => {
                    this.errorMessage = error.errorMessage;
                });
        }
    }
}
