import {map} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {Component, OnInit} from '@angular/core';

import {configs} from '../../../../../../shared/configs';
import {AppSharedService} from '../../../../../../shared/services/app-shared.service';
import {ApartmentService} from '../../services/apartment.service';
import {AuthService} from '../../../../../../shared/services/auth.service';
import {PageChangeEvent} from '../../../../../../shared/models/page-change-event.model';
import {Apartment} from '../../models/apartment.model';
import {Realtor} from '../../models/realtor.model';
import {ApiError} from '../../../../../../shared/models/api-error.model';

@Component({
    selector: 'app-edit-apartment',
    templateUrl: './edit-apartment.component.html',
    styleUrls: ['./edit-apartment.component.css']
})
export class EditApartmentComponent implements OnInit {

    naturalNumberRegex = configs.validations.naturalNumberRegex;
    doubleNumberRegex = configs.validations.doubleNumberRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;
    maxLengthApartmentRooms = configs.validations.maxLengthApartmentRooms;
    maxLengthApartmentArea = configs.validations.maxLengthApartmentArea;
    maxLengthApartmentPrice = configs.validations.maxLengthApartmentPrice;

    apartment: Apartment;
    selectedRealtorId: string;
    realtors: Realtor[] = [];
    errorMessage: string;
    formSubmitted: boolean;

    mapCenterLatitude: number;
    mapCenterLongitude: number;

    blueMarkerIcon = './assets/img/map-blue-marker.png';
    redMarkerIcon = './assets/img/map-red-marker.png';
    mapZoom = configs.constants.mapDefaultZoom;
    mapMinZoom = configs.constants.mapMinZoom;

    constructor(private sharedService: AppSharedService,
                private apartmentService: ApartmentService,
                private authService: AuthService,
                private router: Router,
                private route: ActivatedRoute) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.apartments, configs.breadcrumb.subSections.edit));
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            const id = params['id'];

            this.loadRealtors();

            this.apartmentService.getById(id)
                .pipe(
                    map((apartment: Apartment) => {
                        this.apartment = apartment;
                        this.mapCenterLatitude = this.apartment.latitude;
                        this.mapCenterLongitude = this.apartment.longitude;
                        this.selectedRealtorId = apartment.realtorId;
                    })
                ).subscribe();
        });
    }

    loadRealtors(): void {
        this.apartmentService.getRealtors()
            .pipe(map((realtors: Realtor[]) => {
                this.realtors = realtors;
            }))
            .subscribe();
    }

    onMapClick(event) {
        this.apartment.latitude = event.coords.lat;
        this.apartment.longitude = event.coords.lng;
    }

    submitApartment(valid: boolean) {
        this.formSubmitted = true;
        this.errorMessage = '';

        if (valid) {
            this.apartment.realtorId = this.selectedRealtorId;
            this.apartmentService
                .update(this.apartment.id, this.apartment)
                .subscribe(() => {
                        this.goToApartmentsSearch();
                    },
                    (error: ApiError) => {
                        this.errorMessage = error.errorMessage;
                    });
        }
    }

    goToApartmentsSearch(): void {
        this.router.navigate(['apartments/search']);
    }
}
