import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

import {ModalResponse} from './modal.response.model';
import {ModalInput} from './modal.input.model';

@Component({
    selector: 'app-modal',
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css']
})
export class ModalComponent {

    title: string;
    message: string;

    constructor(
        public dialogRef: MatDialogRef<ModalComponent>,
        @Inject(MAT_DIALOG_DATA) public data: ModalInput) {
        this.title = data.title;
        this.message = data.message;
    }

    ok() {
        const modalResponse = new ModalResponse(true, null);

        this.dialogRef.close(modalResponse);
    }

    cancel() {
        const modalResponse = new ModalResponse(false, null);

        this.dialogRef.close(modalResponse);
    }
}
