import {NgModule} from '@angular/core';
import {AgmCoreModule} from '@agm/core';
import {MatRadioModule} from '@angular/material';
import {environment} from '../../../../../environments/environment';
import {FormsModule} from '@angular/forms';

import {SearchApartmentComponent} from './components/search-apartment/search-apartment.component';
import {ApartmentsRoutingModule} from './apartments-routing.module';
import {EditApartmentComponent} from './components/edit-apartment/edit-apartment.component';
import {AddApartmentComponent} from './components/add-apartment/add-apartment.component';
import {ViewApartmentComponent} from './components/view-apartment/view-apartment.component';
import {ApartmentService} from './services/apartment.service';
import {ApartmentApiService} from './services/apartment-api.service';
import {SharedModule} from '../../../../shared/shared.module';
import {ModalComponent} from '../../../../shared/components/modal/modal.component';

@NgModule({
    imports: [
        SharedModule,
        AgmCoreModule.forRoot({
            apiKey: environment.googleApiKey
        }),
        ApartmentsRoutingModule,
        FormsModule,
        MatRadioModule
    ],
    declarations: [SearchApartmentComponent, EditApartmentComponent, AddApartmentComponent, ViewApartmentComponent],
    entryComponents: [ModalComponent],
    providers: [ApartmentService, ApartmentApiService]
})
export class ApartmentsModule {
}
