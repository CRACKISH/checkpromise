export class ChartDataValue {
    public date: Date;
    public value: any;
}

export class ChartData {
    public label: string;
    public initialData: ChartDataValue;
    public currentData: ChartDataValue;
}
