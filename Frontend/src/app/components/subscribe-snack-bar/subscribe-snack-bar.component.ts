import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-subscribe-snack-bar',
  templateUrl: './subscribe-snack-bar.component.html',
  styleUrls: ['./subscribe-snack-bar.component.scss']
})
export class SubscribeSnackBarComponent {

  constructor(
    public snackBar: MatSnackBar
  ) { }

  public doSubscribe() {
    window.open('https://t.me/checkpromise_info_bot', '_blank');
    this.snackBar.dismiss();
  }

}
