import { Component, Input } from '@angular/core';

import { ChartData } from 'src/app/models/chart-data.model';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent {

  @Input()
  public data: ChartData = new ChartData();

  public getInitialDataValue(): string {
    return this.data.initialData.value + ' ' + this.data.initialData.measure;
  }

  public getCurrentDataValue(): string {
    return this.data.currentData.value + ' ' + this.data.currentData.measure;
  }

  public getArrowClass(): string {
    let arrowClass;
    const initialDataValue = this.data.initialData.value;
    const currentDataValue = this.data.currentData.value;
    const invert = this.data.invertArrow;
    if (currentDataValue > initialDataValue) {
      arrowClass = invert ? 'green-arrow-up-positive' : 'red-arrow-up-negative';
    } else if (currentDataValue < initialDataValue) {
      arrowClass = invert ? 'red-arrow-down-negative' : 'green-arrow-down-positive';
    }
    return arrowClass;
  }

}
