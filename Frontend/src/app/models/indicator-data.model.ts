export enum Measure {
  UAH = '₴',
  USD = '$',
  Percent = '%'
}

export class GraphData {
  public date: string;
  public value: number;
}

export class IndicatorDataValue {
  public date: string;
  public value: number;
  public value2: number;
  public quantity: string;
}

export class IndicatorData {
  public id: number;
  public label: string;
  public invertArrow = false;
  public measure: Measure;
  public measure2: Measure;
  public initialData: IndicatorDataValue;
  public currentData: IndicatorDataValue;
  public source: string;
  public graphData: GraphData[];

  constructor() {
    this.initialData = new IndicatorDataValue();
    this.currentData = new IndicatorDataValue();
  }
}
