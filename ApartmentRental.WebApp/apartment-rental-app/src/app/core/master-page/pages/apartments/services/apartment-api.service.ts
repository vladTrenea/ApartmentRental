import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs/internal/Observable';

import {ApiService} from '../../../../../shared/services/api.service';
import {Apartment} from '../models/apartment.model';
import {Realtor} from '../models/realtor.model';
import {StorageService} from '../../../../../shared/services/storage.service';
import {ApartmentFilter} from '../models/apartment.filter.model';

@Injectable()
export class ApartmentApiService extends ApiService {

    constructor(httpClient: HttpClient, storageService: StorageService) {
        super(httpClient, storageService);
    }

    getFiltered(filter: ApartmentFilter): Observable<Apartment[]> {
        return this.httpClient.get<Apartment[]>(`${this.endpoints.apartments}`,
            {headers: this.getRequestHeaders(), params: this.getFilteringParameters(filter)});
    }

    getRentableFiltered(filter: ApartmentFilter): Observable<Apartment[]> {
        return this.httpClient.get<Apartment[]>(`${this.endpoints.apartments}/rentable`,
            {headers: this.getRequestHeaders(), params: this.getFilteringParameters(filter)});
    }

    getById(id: string): Observable<Apartment> {
        return this.httpClient.get<Apartment>(`${this.endpoints.apartments}/${id}`,
            {headers: this.getRequestHeaders()});
    }

    getRealtors(): Observable<Realtor[]> {
        return this.httpClient.get<Realtor[]>(`${this.endpoints.realtors}`,
            {headers: this.getRequestHeaders()});
    }

    add(apartment: Apartment): Observable<any> {
        return this.httpClient.post(this.endpoints.apartments,
            JSON.stringify(apartment),
            {headers: this.getRequestHeaders()});
    }

    update(id: string, apartment: Apartment): Observable<any> {
        return this.httpClient.put(`${this.endpoints.apartments}/${id}`,
            JSON.stringify(apartment),
            {headers: this.getRequestHeaders()});
    }

    delete(id: string): Observable<any> {
        return this.httpClient.delete(`${this.endpoints.apartments}/${id}`,
            {headers: this.getRequestHeaders()});
    }

    private getFilteringParameters(filter: ApartmentFilter): HttpParams {
        let params = new HttpParams();
        params = params.set('northEastLatitude', filter.northEastLatitude.toString());
        params = params.set('northEastLongitude', filter.northEastLongitude.toString());
        params = params.set('southWestLatitude', filter.southWestLatitude.toString());
        params = params.set('southWestLongitude', filter.southWestLongitude.toString());

        if (filter.minPrice !== null && filter.minPrice !== undefined) {
            params = params.set('minPrice', filter.minPrice.toString());
        }
        if (filter.maxPrice !== null && filter.maxPrice !== undefined) {
            params = params.set('maxPrice', filter.maxPrice.toString());
        }
        if (filter.minArea !== null && filter.minArea !== undefined) {
            params = params.set('minArea', filter.minArea.toString());
        }
        if (filter.maxArea !== null && filter.maxArea !== undefined) {
            params = params.set('maxArea', filter.maxArea.toString());
        }
        if (filter.minNrRooms !== null && filter.minNrRooms !== undefined) {
            params = params.set('minNrRooms', filter.minNrRooms.toString());
        }
        if (filter.maxNrRooms !== null && filter.maxNrRooms !== undefined) {
            params = params.set('maxNrRooms', filter.maxNrRooms.toString());
        }

        return params;
    }
}
