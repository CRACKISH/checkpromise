import { Component } from '@angular/core';

@Component({
    selector: 'app-donate-dialog',
    templateUrl: './donate-dialog.component.html',
    styleUrls: ['./donate-dialog.component.scss'],
    standalone: false
})
export class DonateDialogComponent {

    public readonly cardNumber = '5169 3600 0512 1722';

    public copyCardNumber() {
        navigator.clipboard?.writeText(this.cardNumber);
    }

}
