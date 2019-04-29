import { Component, OnInit } from '@angular/core';

import { ChartData } from './models/chart-data.model';
import { PromiseData } from './models/promise-data.model';
import { DataService } from './services/data.service';
import { MatSlideToggleChange } from '@angular/material';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public chartData: ChartData[];

  public promiseData: PromiseData[];

  public isUSDChecked = false;

  constructor(protected dataService: DataService) {}

  public ngOnInit() {
    this.dataService.get().subscribe(data => {
      this.chartData = data.chartData;
      this.promiseData = data.promiseData;
    });
  }

  public onChangeCurrency(event: MatSlideToggleChange | null) {
    let isUSDChecked = !this.isUSDChecked;
    if (event) {
      isUSDChecked = event.checked;
    }
    this.isUSDChecked = isUSDChecked;

  }

}
