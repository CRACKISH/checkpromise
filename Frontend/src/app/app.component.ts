import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, NavigationStart, NavigationEnd } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

import { SubscribeSnackBarComponent } from './components/subscribe-snack-bar/subscribe-snack-bar.component';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';
import { ChangeCurrencyService } from './services/change-currency.service';
import { delay } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public isUSDChecked = false;

  public isReady = false;

  constructor(
    protected changeCurrencyService: ChangeCurrencyService,
    protected cookieService: CookieService,
    protected dialog: MatDialog,
    protected router: Router,
    protected snackBar: MatSnackBar
  ) {
    this.router.events
      .subscribe(event => {
        console.log(event);
        if (event instanceof NavigationStart) {
          setTimeout(() => this.isReady = false, 0);
        } else if (event instanceof NavigationEnd) {
          setTimeout(() => this.isReady = true, 500);
        }
      });
  }

  protected isNeedShowSubscribeSnackBar(): boolean {
    const tgBotTag = 'utm_source=telegram_bot';
    const viberBotTag = 'utm_source=viber_bot';
    const href = window.location.href;
    const isNotLinkFromTgBot = href.indexOf(tgBotTag) === -1;
    const isNotLinkFromViberBot = href.indexOf(viberBotTag) === -1;
    const hasNotCookie = this.cookieService.get('IsSubscribed') !== 'true';
    return isNotLinkFromViberBot && isNotLinkFromTgBot && hasNotCookie;
  }

  public ngOnInit() {
    if (this.isNeedShowSubscribeSnackBar()) {
      setTimeout(() => this.snackBar.openFromComponent(SubscribeSnackBarComponent), 500);
    }
  }

  public onChangeCurrency(event: MatSlideToggleChange | null) {
    let isUSDChecked = !this.isUSDChecked;
    if (event) {
      isUSDChecked = event.checked;
    }
    this.isUSDChecked = isUSDChecked;
    this.changeCurrencyService.doChange(isUSDChecked);
  }

  public showDonateDialog() {
    this.dialog.open(DonateDialogComponent, {
      autoFocus: false
    });
  }

  public getSwitchCurrencyVisible(): boolean {
    return this.router.url === '/';
  }

  public getFooterAdditionClass(): string {
    return this.router.url === '/' ? '' : 'bottom-position';
  }

}
