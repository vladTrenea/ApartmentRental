import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ProfileComponent} from './profile/profile.component';

const routes: Routes = [
    {
        path: 'profile',
        component: ProfileComponent,
    },
    {
        path: '',
        redirectTo: 'profile',
        pathMatch: 'prefix'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AccountRoutingModule {

}
