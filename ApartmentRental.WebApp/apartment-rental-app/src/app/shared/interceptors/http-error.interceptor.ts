import {Router} from '@angular/router';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/internal/Observable';
import {catchError, finalize, map} from 'rxjs/operators';
import {throwError} from 'rxjs/internal/observable/throwError';
import {of} from 'rxjs/internal/observable/of';

import {PubSubService} from '../services/pub-sub/pub-sub.service';
import {PubSubMessageTypeEnum} from '../services/pub-sub/pub-sub-message-type-enum';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router,
                private pubSubService: PubSubService) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.pubSubService.pub(PubSubMessageTypeEnum.Loading, true);

        return next.handle(request).pipe(
            finalize(() => {
                this.pubSubService.pub(PubSubMessageTypeEnum.Loading, false);
            }),
            catchError((err: HttpErrorResponse) => {
                if (err.error instanceof Error) {
                    // A client-side or network error occurred. Handle it accordingly.
                    console.log('An error occurred:', err.error.message);

                    this.router.navigate(['/unknown-error'], {skipLocationChange: true});
                } else {
                    switch (err.status) {
                        case 401:
                            if (this.router.url === '/login') {
                                break;
                            }
                            this.router.navigate(['/login'], {skipLocationChange: false});
                            return of(err.message);

                        case 403:
                            this.router.navigate(['/forbidden'], {skipLocationChange: true});
                            return of(err.message);

                        case 404:
                            this.router.navigate(['/not-found'], {skipLocationChange: true});
                            return of(err.message);

                        case 400:
                        case 409:
                        case 422:
                            break;

                        case 500:
                            this.router.navigate(['/internal-server-error'], {skipLocationChange: true});
                            return of(err.message);

                        default:
                            this.router.navigate(['/unknown-error'], {skipLocationChange: true});
                            return of(err.message);
                    }
                }

                return throwError(err.error);
            }
        )) as any;
    }
}
