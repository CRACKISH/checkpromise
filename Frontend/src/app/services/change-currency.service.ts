import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChangeCurrencyService {

  private changeSource$ = new BehaviorSubject<boolean>(false);

  private change$ = this.changeSource$.asObservable();

  public doChange(isUSDChecked: boolean) {
    this.changeSource$.next(isUSDChecked);
  }

  public changed(): Observable<boolean> {
    return this.change$;
  }

  public isUSDChecked(): boolean {
    return this.changeSource$.getValue();
  }

}
