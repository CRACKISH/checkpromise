import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSlideToggleChange, MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

import { SubscribeSnackBarComponent } from './components/subscribe-snack-bar/subscribe-snack-bar.component';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';
import { ChangeCurrencyService } from './services/change-currency.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public isUSDChecked = false;

  constructor(
    protected changeCurrencyService: ChangeCurrencyService,
    protected cookieService: CookieService,
    protected dialog: MatDialog,
    protected router: Router,
    protected snackBar: MatSnackBar
  ) {}

  protected isNeedShowSubscribeSnackBar(): boolean {
    const tgBotTag = 'utm_source=telegram_bot';
    const isNotLinkFromTgBot = window.location.href.indexOf(tgBotTag) === -1;
    const hasNotCookie = this.cookieService.get('IsTGSubscribe') !== 'true';
    return isNotLinkFromTgBot && hasNotCookie;
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

}
