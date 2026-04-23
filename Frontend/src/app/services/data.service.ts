import { httpResource } from '@angular/common/http';
import { Injectable, computed } from '@angular/core';

import { Data } from '../models/data.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private readonly resource = httpResource<Data>(() => '../../assets/data/data.json');

  public readonly isLoading = this.resource.isLoading;

  public readonly indicatorData = computed(() => this.resource.value()?.indicatorData ?? []);

  public readonly promiseData = computed(() => this.resource.value()?.promiseData ?? []);

}
