import {Component, OnDestroy, OnInit} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';

import {PubSubService} from './shared/services/pub-sub/pub-sub.service';
import {LanguageService} from './shared/services/language.service';
import {PubSubMessage} from './shared/services/pub-sub/pub-sub-message.model';
import {PubSubMessageTypeEnum} from './shared/services/pub-sub/pub-sub-message-type-enum';
import {configs} from './shared/configs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
    loading = false;
    loadingEventSub: any;
    loadingItems = 0;

    constructor(private translateService: TranslateService,
                private languageService: LanguageService,
                private pubSubService: PubSubService) {
    }

    ngOnInit(): void {
        this.loadingEventSub = this.pubSubService.sub().subscribe((message: PubSubMessage) => {
            if (message.type === PubSubMessageTypeEnum.Loading) {
                if (message.value) {
                    this.loadingItems++;
                } else {
                    this.loadingItems--;
                }
                this.loading = this.loadingItems !== 0;
            }
        });

        this.translateService.setDefaultLang(configs.constants.defaultLanguage);
        const languageCode = this.languageService.getSelectedLanguageCode();
        this.translateService.use(languageCode);
    }

    ngOnDestroy(): void {
        this.loadingEventSub.unsubscribe();
    }
}
