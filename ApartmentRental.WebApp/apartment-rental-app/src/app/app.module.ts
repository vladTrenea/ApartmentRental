import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from '@angular/common/http';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {ngxLoadingAnimationTypes, NgxLoadingModule} from 'ngx-loading';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';

import {AppComponent} from './app.component';
import {CoreModule} from './core/core.module';
import {HttpErrorInterceptor} from './shared/interceptors/http-error.interceptor';
import {AppSharedService} from './shared/services/app-shared.service';

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            }
        }),
        NgxLoadingModule.forRoot({
            animationType: ngxLoadingAnimationTypes.threeBounce,
            backdropBackgroundColour: 'rgba(0,0,0,0.1)',
            primaryColour: '#ffffff',
            secondaryColour: '#ffffff',
            tertiaryColour: '#ffffff'
        }),
        CoreModule
    ],
    providers: [
        AppSharedService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpErrorInterceptor,
            multi: true,
        }],
    bootstrap: [AppComponent]
})
export class AppModule {
}
