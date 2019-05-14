import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Injectable} from '@angular/core';

import {AuthService} from '../services/auth.service';
import {RoleEnum} from '../models/enums/role.enum';

@Injectable()
export class AdminGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const authorization = this.authService.getCurrentUserAuthorization();
        if (authorization === null || authorization.roleId !== RoleEnum.admin) {
            this.router.navigate(['/login']);

            return false;
        }

        return true;
    }
}
