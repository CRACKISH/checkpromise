import { Component, OnInit } from '@angular/core';

import { ChartData } from './models/chart-data.model';
import { PromiseData } from './models/promise-data.model';
import { DataService } from './services/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  public chartData: ChartData[];

  public promiseData: PromiseData[];

  constructor(protected dataService: DataService) {}

  public ngOnInit() {
    this.dataService.get().subscribe(data => {
      this.chartData = data.chartData;
      this.promiseData = data.promiseData;
    });
  }

}
