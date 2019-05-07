import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChartOptions, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import { Subscription } from 'rxjs';

import { DataService } from '../../services/data.service';
import { IndicatorData } from '../../models/indicator-data.model';
import { ChangeCurrencyService } from '../../services/change-currency.service';

@Component({
  selector: 'app-indicator-info-page',
  templateUrl: './indicator-info-page.component.html',
  styleUrls: ['./indicator-info-page.component.scss']
})
export class IndicatorInfoPageComponent implements OnInit, OnDestroy {

  protected id: number;

  protected subscription: Subscription;

  public isUSDChecked = false;

  public indicatorData = new IndicatorData();

  public graphOptions: ChartOptions = {
    responsive: true,
    legend: {
      display: false
    }
  };

  public graphLabels: Label[] = ['2014', '2015', '2016', '2017', '2018', '2019'];

  public graphData: ChartDataSets[] = [{
    data: [],
    fill: false,
    pointBackgroundColor: 'rgb(255,255,255)',
    pointBorderWidth: 1,
    pointHoverRadius: 3,
    pointHoverBackgroundColor: 'rgb(255,255,255)',
    pointHoverBorderWidth: 1,
    pointRadius: 3
  }];

  constructor(
    protected activateRoute: ActivatedRoute,
    protected changeCurrencyService: ChangeCurrencyService,
    protected dataService: DataService
  ) {
    this.id = Number(activateRoute.snapshot.params.id);
  }

  protected buildData() {
    const chartData = [];
    const chartLabels = [];
    let graphDatas = this.indicatorData.graphData;
    graphDatas = graphDatas.sort((item1, item2) => {
      const date1 = new Date(item1.date);
      const date2 = new Date(item2.date);
      if (date1 < date2) { return -1; }
      if (date1 > date2) { return 1; }
      return 0;
    });
    graphDatas.forEach(graphData => {
      chartLabels.push(graphData.date);
      chartData.push(graphData.value);
    });
    this.graphLabels = chartLabels;
    this.graphData[0].data = chartData;
  }

  protected buildGraphConfig() {
    const data = this.graphData[0];
    const initialValue = this.indicatorData.initialData.value;
    const currentValue = this.indicatorData.currentData.value;
    const invert = this.indicatorData.invertArrow;
    let graphColor = 'rgb(190,190,190)';
    const redColor = 'rgb(245,105,105)';
    const greenColor = 'rgb(0,178,89)';
    if (currentValue > initialValue) {
      graphColor = invert ? greenColor : redColor;
    } else if (currentValue < initialValue) {
      graphColor = invert ? redColor : greenColor;
    }
    data.borderColor = graphColor;
    data.pointBorderColor = graphColor;
    data.pointHoverBorderColor = graphColor;
  }

  public ngOnInit() {
    this.dataService.getIndicatorData(this.id).subscribe(indicatorData => {
      this.indicatorData = indicatorData;
      this.buildGraphConfig();
      this.buildData();
    });
    this.subscription = this.changeCurrencyService.changed()
      .subscribe(isUSDChecked => this.isUSDChecked = isUSDChecked);
  }

  public ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
