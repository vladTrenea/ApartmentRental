import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {AddUserComponent} from './add-user/add-user.component';
import {EditUserComponent} from './edit-user/edit-user.component';
import {ListUserComponent} from './list-user/list-user.component';

const routes: Routes = [
    {
        path: 'list',
        component: ListUserComponent,
    },
    {
        path: 'add',
        component: AddUserComponent,
    },
    {
        path: 'edit/:id',
        component: EditUserComponent,
    },
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'prefix'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UsersRoutingModule {

}
