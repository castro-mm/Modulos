import { Component, ElementRef } from '@angular/core';
import { LayoutService } from '../../service/app.layout.service';
import { MenuComponent } from "./menu.component";

@Component({
    selector: 'app-sidebar',
    standalone: true,
    imports: [MenuComponent],
    templateUrl: './sidebar.component.html',
    styles: ``
})
export class SidebarComponent {
    constructor(public layoutService: LayoutService, public el: ElementRef) { }
}
