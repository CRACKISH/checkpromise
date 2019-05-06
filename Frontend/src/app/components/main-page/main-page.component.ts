import { Component, OnInit, Input } from '@angular/core';

import { IndicatorData } from '../../models/indicator-data.model';
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

  public indicatorData: IndicatorData[];

  public promiseData: PromiseData[];

  constructor(
    protected dataService: DataService
  ) {}

  public ngOnInit() {
    this.dataService.get().subscribe(data => {
      this.indicatorData = data.indicatorData;
      this.promiseData = data.promiseData;
    });
  }

}
