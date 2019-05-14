import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {map} from 'rxjs/operators';

import {AppSharedService} from '../../../../../shared/services/app-shared.service';
import {UserService} from '../services/user.service';
import {configs} from '../../../../../shared/configs';
import {PageChangeEvent} from '../../../../../shared/models/page-change-event.model';
import {Role} from '../models/role.model';
import {User} from '../models/user.model';
import {ApiError} from '../../../../../shared/models/api-error.model';

@Component({
    selector: 'app-edit-user',
    templateUrl: './edit-user.component.html',
    styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

    onlyLettersRegex = configs.validations.onlyLettersRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;

    user: User;
    selectedRoleId: number;
    roles: Role[] = [];
    errorMessage: string;
    formSubmitted: boolean;

    constructor(private sharedService: AppSharedService,
                private userService: UserService,
                private router: Router,
                private route: ActivatedRoute) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.users, configs.breadcrumb.subSections.edit));
    }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            const id = params['id'];

            this.loadRoles();
            this.loadUser(id);
        });
    }

    updateUser(valid: boolean): void {
        this.formSubmitted = true;
        this.errorMessage = '';

        if (valid) {
            this.user.roleId = this.selectedRoleId;
            this.userService
                .update(this.user.id, this.user)
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

    private loadUser(id: string): void {
        this.userService.getById(id)
            .pipe(
                map((user: User) => {
                    this.user = user;
                    this.selectedRoleId = this.user.roleId;
                })
            )
            .subscribe();
    }

    private loadRoles(): void {
        this.userService.getUserRoles()
            .pipe(map((roles: Role[]) => {
                this.roles = roles;
            }))
            .subscribe();
    }
}
