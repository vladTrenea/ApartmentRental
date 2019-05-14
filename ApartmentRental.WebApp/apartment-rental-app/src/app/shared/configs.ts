import {environment} from '../../environments/environment';

import {Language} from './models/language.model';
import {LanguageCodeEnum} from './models/enums/language-code.enum';

const apiBaseUrl = environment.apiUrl;

export const configs = {
    apiSettings: {
        endpoints: {
            login: `${apiBaseUrl}/auth/login`,
            register: `${apiBaseUrl}/auth/register`,
            confirmAccount: `${apiBaseUrl}/auth/activate`,
            resetPassword: `${apiBaseUrl}/auth/password/reset`,
            passwordTokenValidity: `${apiBaseUrl}/auth/passwordTokenValidity`,
            changePassword: `${apiBaseUrl}/auth/password/change`,
            apartments: `${apiBaseUrl}/apartments`,
            realtors: `${apiBaseUrl}/users/realtors`,
            users: `${apiBaseUrl}/users`,
            roles: `${apiBaseUrl}/roles`,
            account: `${apiBaseUrl}/auth/account`
        },
        headers: {
            tokenHeaderName: 'Authorization',
            language: 'Accept-Language',
            contentType: 'Content-Type',
            contentDisposition: 'content-disposition',
            contentTypeJsonValue: 'application/json'
        },
        statusCodes: {
            401: 401,
            403: 403,
            404: 404,
            500: 500
        },
        timeout: 60000
    },
    localStorageKeys: {
        userAuthorization: 'apRental.authorization'
    },
    sessionStorageKeys: {
        selectedLanguageCode: 'apRental.language',
    },
    siteLanguages: [
        new Language('EN', LanguageCodeEnum.en),
    ],
    breadcrumb: {
        subSections: {
            add: 'add',
            edit: 'edit',
            list: 'list',
            view: 'view',
            profile: 'profile'
        },
        sections: {
            account: 'My Account',
            apartments: 'Apartments',
            users: 'Users'
        }
    },
    constants: {
        defaultLanguage: LanguageCodeEnum.en,
        mapDefaultLatitude: 45,
        mapDefaultLongitude: 25,
        mapDefaultZoom: 5,
        mapMinZoom: 5,
        modalHeight: '210px',
        modalWidth: '300px'
    },
    validations: {
        maxTextInputLength: 100,
        emailRegex: '^(([^<>()[\\]\\\\.,;:\\s@\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\"]+)*)|(\\".+\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$',
        onlyLettersRegex: '^[a-zA-Z]+$',
        passwordRegex: '^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$',
        naturalNumberRegex: '^(?:0|[1-9][0-9]*)$',
        doubleNumberRegex: '^(?:[1-9]\\d*|0)?(?:\\.\\d+)?$',
        maxLengthApartmentRooms: 2,
        maxLengthApartmentArea: 12,
        maxLengthApartmentPrice: 10
    }
};
