import {NgModule} from '@angular/core';

import {ProfileComponent} from './profile/profile.component';
import {SharedModule} from '../../../../shared/shared.module';
import {AccountRoutingModule} from './account-routing.module';

@NgModule({
    imports: [
        SharedModule,
        AccountRoutingModule,
    ],
    declarations: [ProfileComponent]
})
export class AccountModule {
}
