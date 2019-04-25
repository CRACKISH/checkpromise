export enum Measure {
  UAH = '₴',
  USD = '$',
  Percent = '%'
}

export class ChartDataValue {
    public date: string;
    public measure: Measure = Measure.UAH;
    public value: number;
    public quantity: string;
}

export class ChartData {
    public label: string;
    public invertArrow = false;
    public initialData: ChartDataValue;
    public currentData: ChartDataValue;
}
