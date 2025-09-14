import { sharedConfig } from '@/shared/config/shared.config';
import { Component, OnInit } from '@angular/core';
import { LoadingService } from '../services/loading.service';

@Component({
    selector: 'app-loading',
    imports: [...sharedConfig.imports],
    template: `
        <p-blockUI [blocked]="isLoading">
            <div class="loading-overlay">
                <p-progress-spinner strokeWidth="3" class="custom-spinner" fill="transparent" animationDuration=".5s"/>
            </div>    
        </p-blockUI>
    `,
    styles: `
        .loading-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 9999;
        }
        .custom-spinner {
            width: 50px;
            height: 50px;
        }    
    `
})
export class LoadingComponent implements OnInit {
    isLoading: boolean = false;

    constructor(private loadingService: LoadingService) { }
    
    ngOnInit(): void {
        this.isLoading = this.loadingService.loading();
    }
}
