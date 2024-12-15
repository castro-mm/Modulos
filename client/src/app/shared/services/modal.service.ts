import { Component, Injectable, Type } from '@angular/core';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class ModalService {
    dialogRef: DynamicDialogRef = {} as DynamicDialogRef;

    constructor(private dialogService: DialogService) { }

    show<T = Component>(component: Type<T>, header: string, width: number): Observable<any | undefined> {        
        this.dialogRef = this.dialogService.open(component, {
            header: header,
            width: `${width}%`,
        })

        return this.dialogRef.onClose;
    }

}
