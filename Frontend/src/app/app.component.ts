import { Component, DestroyRef, OnInit, computed, inject, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NavigationEnd, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

import { SubscribeSnackBarComponent } from './components/subscribe-snack-bar/subscribe-snack-bar.component';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';
import { ChangeCurrencyService } from './services/change-currency.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    standalone: false
})
export class AppComponent implements OnInit {

  private readonly changeCurrencyService = inject(ChangeCurrencyService);
  private readonly cookieService = inject(CookieService);
  private readonly dialog = inject(MatDialog);
  private readonly router = inject(Router);
  private readonly snackBar = inject(MatSnackBar);

  public readonly isUSDChecked = this.changeCurrencyService.isUSDChecked;

  private readonly _currentUrl = signal(this.router.url);
  private readonly currentUrl = this._currentUrl.asReadonly();

  private readonly isRootUrl = computed(() => {
    const url = this.currentUrl();
    return url === '/' || url.startsWith('/?');
  });

  public readonly switchCurrencyVisible = this.isRootUrl;

  public readonly footerAdditionClass = computed(() => this.isRootUrl() ? '' : 'bottom-position');

  constructor() {
    const sub = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this._currentUrl.set(this.router.url);
      }
    });
    inject(DestroyRef).onDestroy(() => sub.unsubscribe());
  }

  public ngOnInit() {
    if (this.isNeedShowSubscribeSnackBar()) {
      setTimeout(() => this.snackBar.openFromComponent(SubscribeSnackBarComponent, {
        panelClass: 'subscribe-snack-bar-panel'
      }), 500);
    }
  }

  public onChangeCurrency(event: MatSlideToggleChange | null) {
    const isUSDChecked = event ? event.checked : !this.isUSDChecked();
    this.changeCurrencyService.doChange(isUSDChecked);
  }

  public showDonateDialog() {
    this.dialog.open(DonateDialogComponent, {
      autoFocus: false,
      panelClass: 'donate-dialog-panel'
    });
  }

  private isNeedShowSubscribeSnackBar(): boolean {
    const tgBotTag = 'utm_source=telegram_bot';
    const viberBotTag = 'utm_source=viber_bot';
    const href = window.location.href;
    const isNotLinkFromTgBot = href.indexOf(tgBotTag) === -1;
    const isNotLinkFromViberBot = href.indexOf(viberBotTag) === -1;
    const hasNotCookie = this.cookieService.get('IsSubscribed') !== 'true';
    return isNotLinkFromViberBot && isNotLinkFromTgBot && hasNotCookie;
  }

}
