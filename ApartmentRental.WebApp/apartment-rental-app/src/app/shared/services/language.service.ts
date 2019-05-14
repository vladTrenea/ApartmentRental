import {Injectable} from '@angular/core';
import {TranslateService} from '@ngx-translate/core';

import {configs} from '../configs';
import {Language} from '../models/language.model';
import {LanguageCodeEnum} from '../models/enums/language-code.enum';

@Injectable()
export class LanguageService {

    constructor(private translateService: TranslateService) {

    }

    getLanguages(): Language[] {
        return configs.siteLanguages;
    }

    getSelectedLanguageCode(): LanguageCodeEnum {
        let selectedCode = sessionStorage.getItem(configs.sessionStorageKeys.selectedLanguageCode);
        if (!selectedCode) {
            selectedCode = configs.constants.defaultLanguage;
        }

        return LanguageCodeEnum[selectedCode];
    }

    setUsedLanguage(languageCode: LanguageCodeEnum): void {
        sessionStorage.setItem(configs.sessionStorageKeys.selectedLanguageCode, languageCode);
    }
}
