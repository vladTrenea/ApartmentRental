import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {map} from 'rxjs/operators';

import {User} from '../models/user.model';
import {AppSharedService} from '../../../../../shared/services/app-shared.service';
import {UserService} from '../services/user.service';
import {PageChangeEvent} from '../../../../../shared/models/page-change-event.model';
import {configs} from '../../../../../shared/configs';
import {ApiError} from '../../../../../shared/models/api-error.model';
import {Role} from '../models/role.model';

@Component({
    selector: 'app-add-user',
    templateUrl: './add-user.component.html',
    styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

    emailRegex = configs.validations.emailRegex;
    onlyLettersRegex = configs.validations.onlyLettersRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;

    user: User = new User();
    selectedRoleId: number;
    roles: Role[] = [];
    errorMessage: string;
    formSubmitted: boolean;

    constructor(private sharedService: AppSharedService,
                private userService: UserService,
                private router: Router) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.users, configs.breadcrumb.subSections.add));
    }

    ngOnInit(): void {
        this.loadRoles();
    }

    addUser(valid: boolean): void {
        this.formSubmitted = true;
        this.errorMessage = '';

        if (valid) {
            this.user.roleId = this.selectedRoleId;
            this.userService
                .add(this.user)
                .subscribe(() => {
                        this.goToList();
                    },
                    (error: ApiError) => {
                        this.errorMessage = error.errorMessage;
                    });
        }
    }

    goToList(): void {
        this.router.navigate(['users/list']);
    }

    private loadRoles(): void {
        this.userService.getUserRoles()
            .pipe(map((roles: Role[]) => {
                this.roles = roles;
            }))
            .subscribe();
    }
}
