import { Component, Input } from '@angular/core';

import { IndicatorData, IndicatorDataValue } from '../../models/indicator-data.model';

@Component({
  selector: 'app-indicator',
  templateUrl: './indicator.component.html',
  styleUrls: ['./indicator.component.scss']
})
export class IndicatorComponent {

  @Input()
  public data: IndicatorData;

  @Input()
  public invertValue = false;

  @Input()
  public disableGraphLink = false;

  protected replaceOnNonWhiteSpace(value: string): string {
    return value.replace(/ /g, '\u00a0');
  }

  protected getDataValue(data: IndicatorDataValue): number {
    const value = this.invertValue && this.data.measure2 ? data.value2 : data.value;
    return Number(value);
  }

  protected getChartMeasure(): string {
    return this.invertValue && this.data.measure2 ? this.data.measure2 : this.data.measure;
  }

  protected getDataValueString(data: IndicatorDataValue): string {
    const numberValue: number = this.getDataValue(data);
    let value = numberValue.toLocaleString();
    value = value + ' ' + this.getChartMeasure();
    return this.replaceOnNonWhiteSpace(value);
  }

  public getInitialDataValue(): string {
    return this.getDataValueString(this.data.initialData);
  }

  public getCurrentDataValue(): string {
    return this.getDataValueString(this.data.currentData);
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

  public getVisible(): boolean {
    return !!this.data;
  }

}
