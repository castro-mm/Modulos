import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
    providedIn: 'root'
})
export class BusyService {
    loading: boolean = false;
    busyRequestCount: number = 0;

    constructor(private spinnerService: NgxSpinnerService) {}

    busy() {
        this.busyRequestCount++;
        this.spinnerService.show(undefined, {
            type: 'line-scale-party',
            bdColor: 'rgba(0, 0, 0, 0.8)',
            color: '#fff'
        })
        this.loading = true;
    }

    idle() {
        this.busyRequestCount--;
        if (this.busyRequestCount <= 0) {
            this.busyRequestCount = 0;
            this.loading = false;
            this.spinnerService.hide();
        }
    }
}
