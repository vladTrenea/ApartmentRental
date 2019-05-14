import {Component} from '@angular/core';
import {Router} from '@angular/router';

import {LoginModel} from '../../shared/models/login.model';
import {AuthService} from '../../shared/services/auth.service';
import {ApiError} from '../../shared/models/api-error.model';
import {configs} from '../../shared/configs';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
})
export class LoginComponent {

    emailRegex = configs.validations.emailRegex;

    loginModel: LoginModel = new LoginModel();
    formSubmitted = false;
    errorMessage: string;

    constructor(private authService: AuthService,
                private router: Router) {
    }

    login(valid: boolean): void {
        this.formSubmitted = true;

        if (valid) {
            this.authService.login(this.loginModel).subscribe(
                () => {
                    this.router.navigate(['/apartments']);
                },
                (error: ApiError) => {
                    this.errorMessage = error.errorMessage;
                });
        }
    }
}
