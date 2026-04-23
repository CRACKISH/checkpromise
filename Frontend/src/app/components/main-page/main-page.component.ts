import { Component, computed, inject, signal } from '@angular/core';

import { PromiseStatus } from '../../models/promise-data.model';
import { DataService } from '../../services/data.service';
import { ChangeCurrencyService } from '../../services/change-currency.service';

@Component({
    selector: 'app-main-page',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.scss'],
    standalone: false
})
export class MainPageComponent {

  protected readonly dataService = inject(DataService);
  protected readonly changeCurrencyService = inject(ChangeCurrencyService);

  public readonly isUSDChecked = this.changeCurrencyService.isUSDChecked;
  public readonly isLoading = this.dataService.isLoading;
  public readonly indicatorData = this.dataService.indicatorData;

  private readonly filter = signal<PromiseStatus>(PromiseStatus.Nothing);

  public readonly renderedPromiseData = computed(() => {
    const all = this.dataService.promiseData();
    const type = this.filter();
    return type === PromiseStatus.Nothing ? all : all.filter(item => item.status === type);
  });

  public onPromiseFilterButtonClick(type: PromiseStatus) {
    this.filter.set(type);
  }

}
