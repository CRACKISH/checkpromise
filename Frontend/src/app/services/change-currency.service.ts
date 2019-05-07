import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChangeCurrencyService {

  private changeSource$ = new Subject<boolean>();

  private change$ = this.changeSource$.asObservable();

  public doChange(isUSDChecked: boolean) {
    this.changeSource$.next(isUSDChecked);
  }

  public changed(): Observable<boolean> {
    return this.change$;
  }

}
