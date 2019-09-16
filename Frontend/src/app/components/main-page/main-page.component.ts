import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { IndicatorData } from '../../models/indicator-data.model';
import { PromiseData, PromiseStatus } from '../../models/promise-data.model';
import { DataService } from '../../services/data.service';
import { ChangeCurrencyService } from '../../services/change-currency.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit, OnDestroy {

  protected subscription: Subscription;

  protected indicatorData: IndicatorData[];

  protected promiseData: PromiseData[];

  public isUSDChecked = false;

  public isLoading = false;

  public renderedIndicatorData: IndicatorData[];

  public renderedPromiseData: PromiseData[];

  constructor(
    protected changeCurrencyService: ChangeCurrencyService,
    protected dataService: DataService
  ) {}

  public ngOnInit() {
    this.isLoading = true;
    this.dataService.get().subscribe(data => {
      this.indicatorData = data.indicatorData;
      this.promiseData = data.promiseData;
      this.renderedIndicatorData = this.indicatorData.map(item => item);
      this.renderedPromiseData = this.promiseData.map(item => item);
      this.isLoading = false;
    });
    this.isUSDChecked = this.changeCurrencyService.isUSDChecked();
    this.subscription = this.changeCurrencyService.changed()
      .subscribe(isUSDChecked => this.isUSDChecked = isUSDChecked);
  }

  public ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  public onPromiseFilterButtonClick(type: PromiseStatus) {
    if (type === PromiseStatus.Nothing) {
      this.renderedPromiseData = this.promiseData.map(item => item);
    } else {
      this.renderedPromiseData = this.promiseData.filter(item => item.status === type);
    }
  }

}
