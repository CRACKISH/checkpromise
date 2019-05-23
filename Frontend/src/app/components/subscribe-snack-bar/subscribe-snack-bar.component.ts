import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-subscribe-snack-bar',
  templateUrl: './subscribe-snack-bar.component.html',
  styleUrls: ['./subscribe-snack-bar.component.scss']
})
export class SubscribeSnackBarComponent {

  constructor(
    protected cookieService: CookieService,
    public snackBar: MatSnackBar
  ) { }

  protected setCookie(expires?: number) {
    this.cookieService.set('IsTGSubscribe', 'true', expires);
  }

  public doSubscribe() {
    window.open('https://t.me/checkpromise_info_bot', '_blank');
    this.setCookie(30);
    this.snackBar.dismiss();
  }

  public doCancel() {
    this.setCookie(7);
    this.snackBar.dismiss();
  }

}
