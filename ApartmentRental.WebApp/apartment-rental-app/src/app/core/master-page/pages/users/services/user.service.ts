import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/internal/Observable';

import {UserApiService} from './user-api.service';
import {User} from '../models/user.model';
import {Role} from '../models/role.model';

@Injectable()
export class UserService {

    constructor(private userApiService: UserApiService) {

    }

    getAll(): Observable<User[]> {
        return this.userApiService.getAll();
    }

    getById(id: string): Observable<User> {
        return this.userApiService.getById(id);
    }

    add(user: User): Observable<any> {
        return this.userApiService.add(user);
    }

    update(id: string, user: User): Observable<any> {
        return this.userApiService.update(id, user);
    }

    delete(id: string): Observable<any> {
        return this.userApiService.delete(id);
    }

    getUserRoles(): Observable<Role[]> {
        return this.userApiService.getUserRoles();
    }
}
