import {Component, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {finalize, map} from 'rxjs/operators';
import {LatLngBounds} from '@agm/core';

import {UserAuthorization} from '../../../../../../shared/models/user-authorization.model';
import {AppSharedService} from '../../../../../../shared/services/app-shared.service';
import {ApartmentService} from '../../services/apartment.service';
import {AuthService} from '../../../../../../shared/services/auth.service';
import {PageChangeEvent} from '../../../../../../shared/models/page-change-event.model';
import {configs} from '../../../../../../shared/configs';
import {RoleEnum} from '../../../../../../shared/models/enums/role.enum';
import {Apartment} from '../../models/apartment.model';
import {ApartmentFilter} from '../../models/apartment.filter.model';
import {NgForm} from '@angular/forms';

@Component({
    selector: 'app-search-apartment',
    templateUrl: './search-apartment.component.html',
    styleUrls: ['./search-apartment.component.css']
})
export class SearchApartmentComponent implements OnInit {

    @ViewChild('searchApartmentForm') searchApartmentForm: NgForm;

    naturalNumberRegex = configs.validations.naturalNumberRegex;
    doubleNumberRegex = configs.validations.doubleNumberRegex;

    apartments: Apartment[];
    roleEnum = RoleEnum;
    userAuthorization: UserAuthorization;
    filter: ApartmentFilter = new ApartmentFilter();
    loading = false;

    mapCenterDefaultLatitude = configs.constants.mapDefaultLatitude;
    mapCenterDefaultLongitude = configs.constants.mapDefaultLongitude;
    mapZoom = configs.constants.mapDefaultZoom;
    mapMinZoom = configs.constants.mapMinZoom;

    blueMarkerIcon = './assets/img/map-blue-marker.png';
    redMarkerIcon = './assets/img/map-red-marker.png';

    openedWindow: string;
    openWindow = false;
    isLastEventMarkerClick = false;

    constructor(private sharedService: AppSharedService,
                private authService: AuthService,
                private apartmentService: ApartmentService,
                private router: Router) {
        sharedService.emitPageChange(
            new PageChangeEvent(configs.breadcrumb.sections.apartments, configs.breadcrumb.subSections.list));
    }

    ngOnInit() {
        this.userAuthorization = this.authService.getCurrentUserAuthorization();
    }

    searchApartments(): void {
        this.loading = true;
        if (this.userAuthorization.roleId === RoleEnum.client) {
            this.apartmentService.searchRentableApartments(this.filter).pipe(
                map((response: Apartment[]) => {
                    this.apartments = response;
                    this.openedWindow = null;
                    this.openWindow = false;
                }),
                finalize(() => {
                    this.loading = false;
                })
            ).subscribe();
        } else {
            this.apartmentService.searchApartments(this.filter).pipe(
                map((response: Apartment[]) => {
                    this.apartments = response;
                    this.openedWindow = null;
                    this.openWindow = false;
                }),
                finalize(() => {
                    this.loading = false;
                })
            ).subscribe();
        }
    }

    onBoundsChange($event: LatLngBounds): void {
        this.filter.northEastLatitude = $event.getNorthEast().lat();
        this.filter.northEastLongitude = $event.getNorthEast().lng();
        this.filter.southWestLatitude = $event.getSouthWest().lat();
        this.filter.southWestLongitude = $event.getSouthWest().lng();
    }

    onMapIdle(): void {
        if (!this.loading && !this.isLastEventMarkerClick && this.searchApartmentForm.valid) {
            this.searchApartments();
        }
        this.isLastEventMarkerClick = false;
    }

    openMarkerWindow(id) {
        this.openedWindow = id;
        this.openWindow = true;
        this.isLastEventMarkerClick = true;
    }

    isInfoWindowOpen(id) {
        return this.openedWindow === id && this.openWindow;
    }

    goToAddApartment() {
        this.router.navigate(['apartments/add']);
    }
}
