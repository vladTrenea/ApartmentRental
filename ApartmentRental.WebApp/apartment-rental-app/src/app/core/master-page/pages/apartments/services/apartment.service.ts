import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/internal/Observable';

import {ApartmentApiService} from './apartment-api.service';
import {Apartment} from '../models/apartment.model';
import {Realtor} from '../models/realtor.model';
import {ApartmentFilter} from '../models/apartment.filter.model';

@Injectable()
export class ApartmentService {

    constructor(private apartmentApiService: ApartmentApiService) {

    }

    searchApartments(filter: ApartmentFilter): Observable<Apartment[]> {
        return this.apartmentApiService.getFiltered(filter);
    }

    searchRentableApartments(filter: ApartmentFilter) {
        return this.apartmentApiService.getRentableFiltered(filter);
    }

    getById(id: string): Observable<Apartment> {
        return this.apartmentApiService.getById(id);
    }

    getRealtors(): Observable<Realtor[]> {
        return this.apartmentApiService.getRealtors();
    }

    add(apartment: Apartment): Observable<any> {
        return this.apartmentApiService.add(apartment);
    }

    update(id: string, apartment: Apartment): Observable<any> {
        return this.apartmentApiService.update(id, apartment);
    }

    delete(id: string): Observable<any> {
        return this.apartmentApiService.delete(id);
    }
}
