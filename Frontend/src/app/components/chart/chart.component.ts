import { Component, Input } from '@angular/core';

import { ChartData, ChartDataValue } from 'src/app/models/chart-data.model';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent {

  @Input()
  public data: ChartData = new ChartData();

  public invertValue = false;

  protected getDataValue(data: ChartDataValue): number {
    return this.invertValue ? data.value2 : data.value;
  }

  protected getChartMeasure(): string {
    return this.invertValue ? this.data.measure2 : this.data.measure;
  }

  public getInitialDataValue(): string {
    return this.getDataValue(this.data.initialData) + ' ' + this.getChartMeasure();
  }

  public getCurrentDataValue(): string {
    return this.getDataValue(this.data.currentData) + ' ' + this.getChartMeasure();
  }

  public getArrowClass(): string {
    let arrowClass = 'still-icon';
    const initialDataValue = this.getDataValue(this.data.initialData);
    const currentDataValue = this.getDataValue(this.data.currentData);
    const invert = this.data.invertArrow;
    if (currentDataValue > initialDataValue) {
      arrowClass = invert ? 'green-arrow-up-positive' : 'red-arrow-up-negative';
    } else if (currentDataValue < initialDataValue) {
      arrowClass = invert ? 'red-arrow-down-negative' : 'green-arrow-down-positive';
    }
    return arrowClass;
  }

  public getValueClass(): string {
    return this.data.measure2 ? 'clickable' : '';
  }

  public onValueClick() {
    this.invertValue = !this.invertValue;
  }

}
