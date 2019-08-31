import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { IndicatorData } from '../../models/indicator-data.model';
import { PromiseData } from '../../models/promise-data.model';
import { DataService } from '../../services/data.service';
import { ChangeCurrencyService } from '../../services/change-currency.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit, OnDestroy {

  protected subscription: Subscription;

  public isUSDChecked = false;

  public isLoading = false;

  public indicatorData: IndicatorData[];

  public promiseData: PromiseData[];

  constructor(
    protected changeCurrencyService: ChangeCurrencyService,
    protected dataService: DataService
  ) {}

  public ngOnInit() {
    this.isLoading = true;
    this.dataService.get().subscribe(data => {
      this.indicatorData = data.indicatorData;
      this.promiseData = data.promiseData;
      this.isLoading = false;
    });
    this.isUSDChecked = this.changeCurrencyService.isUSDChecked();
    this.subscription = this.changeCurrencyService.changed()
      .subscribe(isUSDChecked => this.isUSDChecked = isUSDChecked);
  }

  public ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
