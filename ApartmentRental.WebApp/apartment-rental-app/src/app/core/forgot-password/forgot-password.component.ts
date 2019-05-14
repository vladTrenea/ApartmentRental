import {Component} from '@angular/core';

import {ResetPasswordModel} from '../../shared/models/reset-password.model';
import {AuthService} from '../../shared/services/auth.service';
import {ApiError} from '../../shared/models/api-error.model';
import {configs} from '../../shared/configs';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html',
    styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {

    emailRegex = configs.validations.emailRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;

    resetPasswordModel: ResetPasswordModel = new ResetPasswordModel();
    formSubmitted = false;
    errorMessage: string;
    passwordResetRequestSent = false;

    constructor(private authService: AuthService) {
    }

    resetPassword(valid: boolean): void {
        this.formSubmitted = true;

        if (valid) {
            this.authService.resetPassword(this.resetPasswordModel).subscribe(
                () => {
                    this.passwordResetRequestSent = true;
                },
                (error: ApiError) => {
                    this.errorMessage = error.errorMessage;
                });
        }
    }
}
