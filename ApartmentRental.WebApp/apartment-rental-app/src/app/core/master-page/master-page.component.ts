import {Component, OnInit} from '@angular/core';

import {AppSharedService} from '../../shared/services/app-shared.service';

@Component({
    selector: 'app-master-page',
    templateUrl: './master-page.component.html',
    styleUrls: ['./master-page.component.css']
})
export class MasterPageComponent {

    section: string;
    subSection: string;

    constructor(private sharedService: AppSharedService) {
        sharedService.changeEmitted$.subscribe(page => {
            this.section = page.section;
            this.subSection = page.subsection;
        });
    }
}
