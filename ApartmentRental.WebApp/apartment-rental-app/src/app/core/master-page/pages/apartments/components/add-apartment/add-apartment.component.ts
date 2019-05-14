import {Component, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {map} from 'rxjs/operators';

import {ApartmentService} from '../../services/apartment.service';
import {Apartment} from '../../models/apartment.model';
import {AppSharedService} from '../../../../../../shared/services/app-shared.service';
import {PageChangeEvent} from '../../../../../../shared/models/page-change-event.model';
import {configs} from '../../../../../../shared/configs';
import {Realtor} from '../../models/realtor.model';
import {ApiError} from '../../../../../../shared/models/api-error.model';

@Component({
    selector: 'app-add-apartment',
    templateUrl: './add-apartment.component.html',
    styleUrls: ['./add-apartment.component.css']
})
export class AddApartmentComponent implements OnInit {

    naturalNumberRegex = configs.validations.naturalNumberRegex;
    doubleNumberRegex = configs.validations.doubleNumberRegex;
    maxTextInputLength = configs.validations.maxTextInputLength;
    maxLengthApartmentRooms = configs.validations.maxLengthApartmentRooms;
    maxLengthApartmentArea = configs.validations.maxLengthApartmentArea;
    maxLengthApartmentPrice = configs.validations.maxLengthApartmentPrice;

    apartment: Apartment = new Apartment();
    selectedRealtorId: string;
    realtors: Realtor[] = [];
    errorMessage: string;
    formSubmitted: boolean;

    blueMarkerIcon = './assets/img/map-blue-marker.png';
    redMarkerIcon = './assets/img/map-red-marker.png';
    mapCenterLatitude = configs.constants.mapDefaultLatitude;
    mapCenterLongitude = configs.constants.mapDefaultLongitude;
    mapZoom = configs.constants.mapDefaultZoom;
    mapMinZoom = configs.constants.mapMinZoom;

    constructor(private sharedService: AppSharedService,
                private apartmentService: ApartmentService,
                private router: Router) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.apartments, configs.breadcrumb.subSections.add));
    }

    ngOnInit(): void {
        this.loadRealtors();
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
                .add(this.apartment)
                .subscribe(() => {
                        this.goToApartmentsSearch();
                    },
                    (error: ApiError) => {
                        this.errorMessage = error.errorMessage;
                    });
        }
    }

    goToApartmentsSearch() {
        this.router.navigate(['apartments/search']);
    }
}
