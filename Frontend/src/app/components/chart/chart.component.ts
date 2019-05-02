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

  @Input()
  public invertValue = false;

  protected replaceOnNonWhiteSpace(value: string): string {
    return value.replace(/ /g, '\u00a0');
  }

  protected getDataValue(data: ChartDataValue): number {
    return this.invertValue && this.data.measure2 ? data.value2 : data.value;
  }

  protected getChartMeasure(): string {
    return this.invertValue && this.data.measure2 ? this.data.measure2 : this.data.measure;
  }

  public getInitialDataValue(): string {
    const value = this.getDataValue(this.data.initialData) + ' ' + this.getChartMeasure();
    return this.replaceOnNonWhiteSpace(value);
  }

  public getCurrentDataValue(): string {
    const value = this.getDataValue(this.data.currentData) + ' ' + this.getChartMeasure();
    return this.replaceOnNonWhiteSpace(value);
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

}
