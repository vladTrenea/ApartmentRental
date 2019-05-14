import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';

import {AuthService} from '../services/auth.service';
import {RoleEnum} from '../models/enums/role.enum';

@Injectable()
export class RealtorGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const authorization = this.authService.getCurrentUserAuthorization();
        if (authorization === null || authorization.roleId === RoleEnum.client) {
            this.router.navigate(['/login']);

            return false;
        }

        return true;
    }
}
