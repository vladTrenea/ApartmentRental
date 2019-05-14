import {Injectable} from '@angular/core';

import {UserAuthorization} from '../models/user-authorization.model';
import {configs} from '../configs';

@Injectable()
export class StorageService {
    constructor() {

    }

    getUserAuthorization(): UserAuthorization {
        return JSON.parse(localStorage.getItem(configs.localStorageKeys.userAuthorization));
    }

    setUserAuthorization(authorization: UserAuthorization) {
        localStorage.setItem(configs.localStorageKeys.userAuthorization, JSON.stringify(authorization));
    }

    removeUserAuthorization() {
        localStorage.removeItem(configs.localStorageKeys.userAuthorization);
    }
}
