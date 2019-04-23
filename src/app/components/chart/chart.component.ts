import { Component, Input } from '@angular/core';

import { ChartData } from 'src/app/models/chart-data.model';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.scss']
})
export class ChartComponent {

  @Input()
  public label = '';

  @Input()
  public data: ChartData = new ChartData();

}
