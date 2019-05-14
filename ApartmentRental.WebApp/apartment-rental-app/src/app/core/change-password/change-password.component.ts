import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {map} from 'rxjs/operators';

import {AuthService} from '../../shared/services/auth.service';
import {ChangePasswordModel} from '../../shared/models/change-password.model';
import {PasswordTokenValidityModel} from '../../shared/models/password-token-validity.model';
import {ApiError} from '../../shared/models/api-error.model';
import {configs} from '../../shared/configs';

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

    passwordRegex = configs.validations.passwordRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;

    changePasswordModel: ChangePasswordModel = new ChangePasswordModel();
    errorMessage = '';
    formSubmitted = false;
    passwordTokenValid = false;
    passwordChanged = false;

    resetPasswordToken: string;

    constructor(private authService: AuthService,
                private router: Router,
                private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.resetPasswordToken = params['token'];

            this.authService.getResetPasswordTokenValidity(this.resetPasswordToken)
                .pipe(
                    map((tokenValidity: PasswordTokenValidityModel) => {
                        this.passwordTokenValid = tokenValidity.valid;
                    }))
                .subscribe();
        });
    }

    changePassword(valid: boolean) {
        this.formSubmitted = true;
        this.errorMessage = '';

        if (valid) {
            this.authService.changePassword(this.resetPasswordToken, this.changePasswordModel)
                .subscribe(() => {
                        this.passwordChanged = true;
                    },
                    (error: ApiError) => {
                        this.errorMessage = error.errorMessage;
                    });
        }
    }
}
