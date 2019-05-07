import { Component, Input } from '@angular/core';

import { IndicatorData, IndicatorDataValue } from '../../models/indicator-data.model';

@Component({
  selector: 'app-indicator',
  templateUrl: './indicator.component.html',
  styleUrls: ['./indicator.component.scss']
})
export class IndicatorComponent {

  @Input()
  public data: IndicatorData = new IndicatorData();

  @Input()
  public invertValue = false;

  @Input()
  public disableGraphLink = false;

  protected replaceOnNonWhiteSpace(value: string): string {
    return value.replace(/ /g, '\u00a0');
  }

  protected getDataValue(data: IndicatorDataValue): number {
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

  public getLabelClass(): string {
    return this.disableGraphLink ? '' : 'clickable';
  }

}
