import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';

import {SearchApartmentComponent} from './components/search-apartment/search-apartment.component';
import {AddApartmentComponent} from './components/add-apartment/add-apartment.component';
import {ViewApartmentComponent} from './components/view-apartment/view-apartment.component';
import {EditApartmentComponent} from './components/edit-apartment/edit-apartment.component';
import {RealtorGuard} from '../../../../shared/guards/realtor.guard';

const routes: Routes = [
    {
        path: 'search',
        component: SearchApartmentComponent,
    },
    {
        path: 'add',
        component: AddApartmentComponent,
        canActivate: [RealtorGuard]
    },
    {
        path: 'view/:id',
        component: ViewApartmentComponent
    },
    {
        path: 'edit/:id',
        component: EditApartmentComponent,
        canActivate: [RealtorGuard]
    },
    {
        path: '',
        redirectTo: 'search',
        pathMatch: 'prefix'
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ApartmentsRoutingModule {

}
