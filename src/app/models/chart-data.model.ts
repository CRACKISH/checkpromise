export enum Currency {
  UAH = '₴',
  USD = '$'
}

export class ChartDataValue {
    public date: string;
    public currency: Currency = Currency.UAH;
    public value: any;
}

export class ChartData {
    public label: string;
    public invertArrow: boolean;
    public initialData: ChartDataValue;
    public currentData: ChartDataValue;
}
