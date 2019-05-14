import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';

import {configs} from '../configs';
import {StorageService} from './storage.service';

@Injectable()
export class ApiService {

    protected endpoints = configs.apiSettings.endpoints;

    constructor(protected httpClient: HttpClient,
                protected storageService: StorageService) {
    }

    protected getRequestHeaders(): HttpHeaders {
        let headers = new HttpHeaders();
        headers = headers.append(configs.apiSettings.headers.contentType, configs.apiSettings.headers.contentTypeJsonValue);

        let language = sessionStorage.getItem(configs.sessionStorageKeys.selectedLanguageCode);
        if (!language) {
            language = configs.constants.defaultLanguage;
        }
        headers = headers.append(configs.apiSettings.headers.language, language);

        const authorization = this.storageService.getUserAuthorization();
        if (authorization) {
            headers = headers.append(configs.apiSettings.headers.tokenHeaderName, `Bearer ${authorization.token}`);
        }

        return headers;
    }
}
