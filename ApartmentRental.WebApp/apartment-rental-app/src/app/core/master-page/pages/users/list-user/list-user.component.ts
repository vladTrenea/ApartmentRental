import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Router} from '@angular/router';
import {map} from 'rxjs/operators';

import {AppSharedService} from '../../../../../shared/services/app-shared.service';
import {PageChangeEvent} from '../../../../../shared/models/page-change-event.model';
import {configs} from '../../../../../shared/configs';
import {UserService} from '../services/user.service';
import {User} from '../models/user.model';
import {ModalComponent} from '../../../../../shared/components/modal/modal.component';
import {ApiError} from '../../../../../shared/models/api-error.model';

@Component({
    selector: 'app-list-user',
    templateUrl: './list-user.component.html',
    styleUrls: ['./list-user.component.css']
})
export class ListUserComponent implements OnInit {

    users: User[];

    constructor(private sharedService: AppSharedService,
                private userService: UserService,
                private router: Router,
                private dialog: MatDialog) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.users, configs.breadcrumb.subSections.list));
    }

    ngOnInit(): void {
        this.loadUsers();
    }

    goToAddUser(): void {
        this.router.navigate(['users/add']);
    }

    goToEditUser(id: string): void {
        this.router.navigate([`users/edit/${id}`]);
    }

    onDeleteUserClick(id: string): void {
        const dialogRef = this.dialog.open(ModalComponent, {
            height: configs.constants.modalHeight,
            width: configs.constants.modalWidth,
            data: {title: 'Delete', message: 'Are you sure you want to delete this user?'}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result && result.isCallback) {
                this.userService.delete(id)
                    .subscribe(() => {
                            const user = this.users.find(u => u.id === id);
                            this.users.splice(this.users.indexOf(user), 1);
                        },
                        (error: ApiError) => {
                            alert(error.errorMessage);
                        });
            }
        });
    }

    private loadUsers() {
        this.userService.getAll()
            .pipe(
                map((users: User[]) => {
                    this.users = users;
                })
            )
            .subscribe();
    }
}
