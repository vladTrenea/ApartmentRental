import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import {AuthService} from '../../shared/services/auth.service';
import {ApiError} from '../../shared/models/api-error.model';

@Component({
    selector: 'app-confirm-account',
    templateUrl: './confirm-account.component.html',
    styleUrls: ['./confirm-account.component.css']
})
export class ConfirmAccountComponent implements OnInit {

    confirmed = false;
    errorMessage = '';

    constructor(private authService: AuthService,
                private router: Router,
                private route: ActivatedRoute) {
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            const emailToken = params['token'];

            this.authService.confirmAccount(emailToken)
                .subscribe(() => {
                        this.confirmed = true;
                    },
                    (error: ApiError) => {
                        this.errorMessage = error.errorMessage;
                    });
        });
    }
}
