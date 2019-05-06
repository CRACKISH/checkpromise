import { Component, OnInit, Input } from '@angular/core';

import { ChartData } from '../../models/chart-data.model';
import { PromiseData } from '../../models/promise-data.model';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  @Input()
  public isUSDChecked = false;

  public chartData: ChartData[];

  public promiseData: PromiseData[];

  constructor(
    protected dataService: DataService
  ) {}

  public ngOnInit() {
    this.dataService.get().subscribe(data => {
      this.chartData = data.chartData;
      this.promiseData = data.promiseData;
    });
  }

}
