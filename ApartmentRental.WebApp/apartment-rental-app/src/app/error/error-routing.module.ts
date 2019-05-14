import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {ErrorComponent} from './error.component';
import {NotFoundComponent} from './not-found/not-found.component';
import {InternalServerErrorComponent} from './internal-server-error/internal-server-error.component';
import {UnknownErrorComponent} from './unknown-error/unknown-error.component';
import {ForbiddenComponent} from './forbidden/forbidden.component';

const routes: Routes = [
    {
        path: '',
        component: ErrorComponent,
        children: [
            {
                path: 'forbidden',
                component: ForbiddenComponent
            },
            {
                path: 'not-found',
                component: NotFoundComponent
            },
            {
                path: 'internal-server-error',
                component: InternalServerErrorComponent
            },
            {
                path: 'unknown-error',
                component: UnknownErrorComponent
            },
            {
                path: '**',
                component: NotFoundComponent
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ErrorRoutingModule {

}
