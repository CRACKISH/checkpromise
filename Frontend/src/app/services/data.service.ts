import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, pipe } from 'rxjs';
import { share, map } from 'rxjs/operators';

import { Data } from '../models/data.model';
import { IndicatorData } from '../models/indicator-data.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  protected dataResponse$: Observable<Data>;

  constructor(private httpClient: HttpClient) { }

  public get(): Observable<Data> {
    if (!this.dataResponse$) {
      this.dataResponse$ = this.httpClient.get<Data>('../../assets/data/data.json').pipe(
        share()
      );
    }
    return this.dataResponse$;
  }

  public getIndicatorData(id: number): Observable<IndicatorData> {
    return this.get().pipe(
      map(data => data.chartData.find(item => item.id === id))
    );
  }

}
