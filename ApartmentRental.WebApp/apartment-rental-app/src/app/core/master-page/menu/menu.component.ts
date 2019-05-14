import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';

import {UserAuthorization} from '../../../shared/models/user-authorization.model';
import {AuthService} from '../../../shared/services/auth.service';
import {configs} from '../../../shared/configs';
import {AppSharedService} from '../../../shared/services/app-shared.service';
import {RoleEnum} from '../../../shared/models/enums/role.enum';

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

    sections = configs.breadcrumb.sections;
    roles = RoleEnum;

    userAuthorization: UserAuthorization;
    section: string;
    subSection: string;
    topMenuOpened = false;

    constructor(private authService: AuthService,
                private sharedService: AppSharedService,
                private router: Router) {
        sharedService.changeEmitted$.subscribe(page => {
            this.section = page.section;
            this.subSection = page.subsection;
        });
    }

    ngOnInit() {
        this.userAuthorization = this.authService.getCurrentUserAuthorization();
    }

    openTopMenu() {
        this.topMenuOpened = !this.topMenuOpened;
    }

    logout() {
        this.authService.logout();
        this.router.navigate(['/login']);
    }
}
