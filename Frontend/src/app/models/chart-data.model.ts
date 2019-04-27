export enum Measure {
  UAH = '₴',
  USD = '$',
  Percent = '%'
}

export class ChartDataValue {
    public date: string;
    public value: number;
    public value2: number;
    public quantity: string;
}

export class ChartData {
    public label: string;
    public invertArrow = false;
    public measure: Measure;
    public initialData: ChartDataValue;
    public currentData: ChartDataValue;
    public source: string;
}
