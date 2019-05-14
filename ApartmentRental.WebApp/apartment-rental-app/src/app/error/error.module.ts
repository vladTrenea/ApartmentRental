import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {NgModule} from '@angular/core';

import {SharedModule} from '../shared/shared.module';
import {ErrorComponent} from './error.component';
import {ErrorRoutingModule} from './error-routing.module';
import {NotFoundComponent} from './not-found/not-found.component';
import {InternalServerErrorComponent} from './internal-server-error/internal-server-error.component';
import {UnknownErrorComponent} from './unknown-error/unknown-error.component';
import {ForbiddenComponent} from './forbidden/forbidden.component';

@NgModule({
    imports: [
        CommonModule,
        ErrorRoutingModule,
        SharedModule
    ],
    declarations: [
        ErrorComponent,
        NotFoundComponent,
        InternalServerErrorComponent,
        UnknownErrorComponent,
        ForbiddenComponent
    ],
    exports: [
        RouterModule
    ],
    providers: []
})
export class ErrorModule {
}
