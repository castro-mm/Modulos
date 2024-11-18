import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
    selector: 'app-not-found',
    standalone: true,
    imports: [RouterLink, ButtonModule],
    templateUrl: './not-found.component.html',
    styles: ``
})
export class NotFoundComponent {

}
