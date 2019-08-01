import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
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
    this.cookieService.set('IsSubscribed', 'true', expires);
  }

  protected doSubscribe(url: string) {
    window.open(url, '_blank');
    this.setCookie(30);
    this.snackBar.dismiss();
  }

  public doTGSubscribe() {
    this.doSubscribe('https://t.me/checkpromise_info_bot');
  }

  public doViberSubscribe() {
    this.doSubscribe('viber://pa?chatURI=checkpromise_info');
  }

  public doCancel() {
    this.setCookie(7);
    this.snackBar.dismiss();
  }

}
