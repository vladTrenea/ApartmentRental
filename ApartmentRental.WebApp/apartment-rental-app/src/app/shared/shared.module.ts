import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {NgModule} from '@angular/core';
import {TranslateModule} from '@ngx-translate/core';
import {MatDialogModule} from '@angular/material';

import {AuthService} from './services/auth.service';
import {AuthApiService} from './services/auth-api.service';
import {StorageService} from './services/storage.service';
import {ApiService} from './services/api.service';
import {PubSubService} from './services/pub-sub/pub-sub.service';
import {LanguageService} from './services/language.service';
import {NotEmptyDirective} from './directives/not-empty.directive';
import {AuthGuard} from './guards/auth.guard';
import {RealtorGuard} from './guards/realtor.guard';
import {AdminGuard} from './guards/admin.guard';
import {ModalComponent} from './components/modal/modal.component';
import {EqualValidatorDirective} from './directives/equal-validator.directive';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        TranslateModule,
        MatDialogModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        TranslateModule,
        NotEmptyDirective,
        EqualValidatorDirective
    ],
    declarations: [NotEmptyDirective, EqualValidatorDirective, ModalComponent],
    providers: [
        ApiService,
        AuthService,
        AuthApiService,
        StorageService,
        PubSubService,
        LanguageService,
        AuthGuard,
        RealtorGuard,
        AdminGuard
    ],
    entryComponents: []
})
export class SharedModule {
}
