import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/internal/Observable';

import {ApiService} from '../../../../../shared/services/api.service';
import {StorageService} from '../../../../../shared/services/storage.service';
import {User} from '../models/user.model';
import {Role} from '../models/role.model';

@Injectable()
export class UserApiService extends ApiService {
    constructor(httpClient: HttpClient, storageService: StorageService) {
        super(httpClient, storageService);
    }

    getAll(): Observable<User[]> {
        return this.httpClient.get<User[]>(`${this.endpoints.users}`,
            {headers: this.getRequestHeaders()});
    }

    getById(id: string): Observable<User> {
        return this.httpClient.get<User>(`${this.endpoints.users}/${id}`,
            {headers: this.getRequestHeaders()});
    }

    add(user: User): Observable<any> {
        return this.httpClient.post(this.endpoints.users,
            JSON.stringify(user),
            {headers: this.getRequestHeaders()});
    }

    update(id: string, user: User): Observable<any> {
        return this.httpClient.put(`${this.endpoints.users}/${id}`,
            JSON.stringify(user),
            {headers: this.getRequestHeaders()});
    }

    delete(id: string): Observable<any> {
        return this.httpClient.delete(`${this.endpoints.users}/${id}`,
            {headers: this.getRequestHeaders()});
    }

    getUserRoles(): Observable<Role[]> {
        return this.httpClient.get<Role[]>(`${this.endpoints.roles}`,
            {headers: this.getRequestHeaders()});
    }
}
