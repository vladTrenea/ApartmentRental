import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {MatDialogModule, MatTooltipModule} from '@angular/material';

import {SharedModule} from '../../../../shared/shared.module';
import {ModalComponent} from '../../../../shared/components/modal/modal.component';
import {UsersRoutingModule} from './users-routing.module';
import {AddUserComponent} from './add-user/add-user.component';
import {EditUserComponent} from './edit-user/edit-user.component';
import {ListUserComponent} from './list-user/list-user.component';
import {UserService} from './services/user.service';
import {UserApiService} from './services/user-api.service';

@NgModule({
    imports: [
        SharedModule,
        UsersRoutingModule,
        FormsModule,
        MatDialogModule,
        MatTooltipModule
    ],
    declarations: [AddUserComponent, EditUserComponent, ListUserComponent],
    entryComponents: [ModalComponent],
    providers: [UserService, UserApiService]
})
export class UsersModule {
}
