import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSlideToggleChange, MatSnackBar } from '@angular/material';

import { SubscribeSnackBarComponent } from './components/subscribe-snack-bar/subscribe-snack-bar.component';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public isUSDChecked = false;

  constructor(
    protected dialog: MatDialog,
    protected snackBar: MatSnackBar
  ) {}

  public ngOnInit() {
    setTimeout(() => {
      this.snackBar.openFromComponent(SubscribeSnackBarComponent, null);
    }, 500);
  }

  public onChangeCurrency(event: MatSlideToggleChange | null) {
    let isUSDChecked = !this.isUSDChecked;
    if (event) {
      isUSDChecked = event.checked;
    }
    this.isUSDChecked = isUSDChecked;
  }

  public showDonateDialog() {
    this.dialog.open(DonateDialogComponent, {
      autoFocus: false
    });
  }

}
