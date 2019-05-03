import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSlideToggleChange, MatSnackBar } from '@angular/material';

import { ChartData } from './models/chart-data.model';
import { PromiseData } from './models/promise-data.model';
import { DataService } from './services/data.service';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';
import { SubscribeSnackBarComponent } from './components/subscribe-snack-bar/subscribe-snack-bar.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public chartData: ChartData[];

  public promiseData: PromiseData[];

  public isUSDChecked = false;

  constructor(
    protected dataService: DataService,
    protected dialog: MatDialog,
    protected snackBar: MatSnackBar
  ) {}

  public ngOnInit() {
    this.dataService.get().subscribe(data => {
      this.chartData = data.chartData;
      this.promiseData = data.promiseData;
      this.snackBar.openFromComponent(SubscribeSnackBarComponent, null);
    });
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
