import {Component, OnInit} from '@angular/core';

import {AppSharedService} from '../../../../../shared/services/app-shared.service';
import {PageChangeEvent} from '../../../../../shared/models/page-change-event.model';
import {configs} from '../../../../../shared/configs';
import {ApiError} from '../../../../../shared/models/api-error.model';
import {AuthService} from '../../../../../shared/services/auth.service';
import {UserAuthorization} from '../../../../../shared/models/user-authorization.model';
import {AccountModel} from '../models/account.model';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

    onlyLettersRegex = configs.validations.onlyLettersRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;

    userAuthorization: UserAuthorization;
    account: AccountModel;
    errorMessage: string;
    formSubmitted: boolean;
    saved = false;

    constructor(private sharedService: AppSharedService,
                private authService: AuthService) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.account, configs.breadcrumb.subSections.profile));
    }

    ngOnInit() {
        this.userAuthorization = this.authService.getCurrentUserAuthorization();
        this.account = new AccountModel();
        this.account.firstName = this.userAuthorization.firstName;
        this.account.lastName = this.userAuthorization.lastName;
    }

    save(valid: boolean): void {
        this.formSubmitted = true;
        this.errorMessage = '';
        this.saved = false;

        if (valid) {
            this.authService
                .updateAccount(this.account)
                .subscribe(() => {
                        this.saved = true;
                    },
                    (error: ApiError) => {
                        this.errorMessage = error.errorMessage;
                    });
        }
    }
}
