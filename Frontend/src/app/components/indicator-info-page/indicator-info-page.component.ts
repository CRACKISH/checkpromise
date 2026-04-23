import { Component, computed, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ChartConfiguration } from 'chart.js';

import { DataService } from '../../services/data.service';
import { ChangeCurrencyService } from '../../services/change-currency.service';
import { IndicatorData, MediaInfo } from '../../models/indicator-data.model';

@Component({
    selector: 'app-indicator-info-page',
    templateUrl: './indicator-info-page.component.html',
    styleUrls: ['./indicator-info-page.component.scss'],
    standalone: false
})
export class IndicatorInfoPageComponent {

  private readonly dataService = inject(DataService);
  private readonly id = Number(inject(ActivatedRoute).snapshot.params['id']);

  public readonly isLoading = this.dataService.isLoading;
  public readonly isUSDChecked = inject(ChangeCurrencyService).isUSDChecked;

  public readonly indicatorData = computed(
    () => this.dataService.indicatorData().find(item => item.id === this.id) ?? new IndicatorData()
  );

  public readonly graphType = 'line' as const;

  public readonly graphOptions: ChartConfiguration<'line'>['options'] = {
    responsive: true,
    plugins: {
      legend: { display: false }
    }
  };

  public readonly graphData = computed<ChartConfiguration<'line'>['data']>(() => {
    const indicator = this.indicatorData();
    const points = (indicator.graphData ?? [])
      .slice()
      .sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());
    const color = this.resolveGraphColor(indicator);
    return {
      labels: points.map(p => p.date),
      datasets: [{
        data: points.map(p => p.value),
        fill: false,
        borderColor: color,
        pointBackgroundColor: 'rgb(255,255,255)',
        pointBorderColor: color,
        pointBorderWidth: 2,
        pointHoverRadius: 3,
        pointHoverBackgroundColor: 'rgb(255,255,255)',
        pointHoverBorderColor: color,
        pointHoverBorderWidth: 2,
        pointRadius: 3
      }]
    };
  });

  private resolveGraphColor(indicator: IndicatorData): string {
    const initialValue = indicator.initialData?.value;
    const currentValue = indicator.currentData?.value;
    if (initialValue == null || currentValue == null) {
      return 'rgb(190,190,190)';
    }
    const redColor = 'rgb(245,105,105)';
    const greenColor = 'rgb(0,178,89)';
    const invert = indicator.invertArrow;
    if (currentValue > initialValue) {
      return invert ? greenColor : redColor;
    }
    if (currentValue < initialValue) {
      return invert ? redColor : greenColor;
    }
    return 'rgb(190,190,190)';
  }

  public getInfoMediaVisible() {
    const mediaInfoData = this.indicatorData().mediaInfoData;
    return mediaInfoData && mediaInfoData.length;
  }

  public getMediaInfoCaption(mediaInfo: MediaInfo): string {
    return mediaInfo.date + ' - ' + mediaInfo.caption;
  }

}
