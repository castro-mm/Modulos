import { Component } from '@angular/core';
import { LayoutService } from '../../service/app.layout.service';

@Component({
    selector: 'app-footer',
    standalone: true,
    imports: [],
    templateUrl: './footer.component.html',
    styles: ``
})
export class FooterComponent {

    constructor(public layoutService: LayoutService) { }
}
