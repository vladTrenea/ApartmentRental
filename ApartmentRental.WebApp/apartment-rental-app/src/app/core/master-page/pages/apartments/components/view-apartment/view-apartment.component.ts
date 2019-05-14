import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {map} from 'rxjs/operators';
import {MatDialog} from '@angular/material';

import {ApartmentService} from '../../services/apartment.service';
import {Apartment} from '../../models/apartment.model';
import {configs} from '../../../../../../shared/configs';
import {AppSharedService} from '../../../../../../shared/services/app-shared.service';
import {PageChangeEvent} from '../../../../../../shared/models/page-change-event.model';
import {UserAuthorization} from '../../../../../../shared/models/user-authorization.model';
import {AuthService} from '../../../../../../shared/services/auth.service';
import {RoleEnum} from '../../../../../../shared/models/enums/role.enum';
import {ModalComponent} from '../../../../../../shared/components/modal/modal.component';
import {ApiError} from '../../../../../../shared/models/api-error.model';

@Component({
    selector: 'app-view-apartment',
    templateUrl: './view-apartment.component.html',
    styleUrls: ['./view-apartment.component.css']
})
export class ViewApartmentComponent implements OnInit {

    roleEnum = RoleEnum;
    userAuthorization: UserAuthorization;
    apartment: Apartment;

    blueMarkerIcon = './assets/img/map-blue-marker.png';
    redMarkerIcon = './assets/img/map-red-marker.png';

    mapZoom = configs.constants.mapDefaultZoom;
    mapMinZoom = configs.constants.mapMinZoom;

    constructor(private sharedService: AppSharedService,
                private apartmentService: ApartmentService,
                private authService: AuthService,
                private router: Router,
                private route: ActivatedRoute,
                private dialog: MatDialog) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.apartments, configs.breadcrumb.subSections.view));
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            const id = params['id'];

            this.apartmentService.getById(id)
                .pipe(
                    map((apartment: Apartment) => {
                        this.apartment = apartment;
                    })
                ).subscribe();
        });
        this.userAuthorization = this.authService.getCurrentUserAuthorization();
    }

    onDeleteClick() {
        const dialogRef = this.dialog.open(ModalComponent, {
            height: configs.constants.modalHeight,
            width: configs.constants.modalWidth,
            data: {title: 'Delete', message: 'Are you sure you want to delete this apartment?'}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result && result.isCallback) {
                this.apartmentService.delete(this.apartment.id)
                    .subscribe(() => {
                            this.goToApartmentsSearch();
                        },
                        (error: ApiError) => {
                            alert(error.errorMessage);
                        });
            }
        });
    }

    goToApartmentsSearch(): void {
        this.router.navigate(['apartments/search']);
    }

    goToEditApartment(): void {
        this.router.navigate([`apartments/edit/${this.apartment.id}`]);
    }
}
