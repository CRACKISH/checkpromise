import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChartOptions, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';

import { DataService } from '../../services/data.service';
import { formatDate } from '@angular/common';
import { ChartData } from '../../models/chart-data.model';

@Component({
  selector: 'app-chart-info-page',
  templateUrl: './chart-info-page.component.html',
  styleUrls: ['./chart-info-page.component.scss']
})
export class ChartInfoPageComponent implements OnInit {

  protected id: number;

  public chartData = new ChartData();

  public graphOptions: ChartOptions = {
    responsive: true,
    legend: {
      display: false
    }
  };

  public graphLabels: Label[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];

  public graphData: ChartDataSets[] = [{
    data: [65, 59, 80, 81, 56, 55, 40],
    fill: false,
    borderColor: 'rgb(245,105,105)',
    pointBackgroundColor: 'rgb(255,255,255)',
    pointBorderColor: 'rgb(245,105,105)',
    pointBorderWidth: 1,
    pointHoverRadius: 5,
    pointHoverBackgroundColor: 'rgb(255,255,255)',
    pointHoverBorderColor: 'rgb(245,105,105)',
    pointHoverBorderWidth: 1,
    pointRadius: 5
  }];

  constructor(
    protected activateRoute: ActivatedRoute,
    protected dataService: DataService
  ) {
    this.id = Number(activateRoute.snapshot.params.id);
  }

  protected buildData() {
    const startDate = new Date(2014, 5, 1);
    const endDate = new Date(2019, 5, 1);
    const dateArray: Date[] = [];
    while (startDate <= endDate) {
      dateArray.push(new Date(startDate));
      startDate.setMonth(startDate.getMonth() + 1);
    }

    const chartData = [];
    const chartLabels = [];
    dateArray.forEach(date => {
      chartLabels.push(formatDate(date, 'dd-mm-yyyy', 'en-us'));
      chartData.push(Math.random());
    });
    this.graphLabels = chartLabels;
    this.graphData[0].data = chartData;
  }

  public ngOnInit() {
    this.dataService.getChartData(this.id).subscribe(chartData => {
      this.chartData = chartData;
      this.buildData();
    });
  }

}
