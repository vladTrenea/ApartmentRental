import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Injectable} from '@angular/core';

import {AuthService} from '../services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const authorization = this.authService.getCurrentUserAuthorization();
        if (authorization === null) {
            this.router.navigate(['/login']);

            return false;
        }

        return true;
    }
}
