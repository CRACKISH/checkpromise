import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { share } from 'rxjs/operators';

import { Data } from '../models/data.model';

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

}
