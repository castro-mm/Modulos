import { Component, Injectable, Type } from '@angular/core';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';

@Injectable({
    providedIn: 'root',
})
export class ModalService {
    dialogRef: DynamicDialogRef = {} as DynamicDialogRef;

    constructor(private dialogService: DialogService) { }

    show<T = Component>(component: Type<T>, header: string, width: number) {        
        this.dialogRef = this.dialogService.open(component, {
            header: header,
            width: `${width}px`,
        })
    }
}
