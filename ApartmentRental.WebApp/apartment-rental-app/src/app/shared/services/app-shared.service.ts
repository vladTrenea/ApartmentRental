import {Injectable} from '@angular/core';
import {Subject} from 'rxjs/internal/Subject';

import {PageChangeEvent} from '../models/page-change-event.model';

@Injectable()
export class AppSharedService {
    private emitChangesSource = new Subject<any>();

    changeEmitted$ = this.emitChangesSource.asObservable();

    emitPageChange(event: PageChangeEvent) {
        this.emitChangesSource.next(event);
    }
}
