import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ChangeCurrencyService {

  private readonly _isUSDChecked = signal(false);

  public readonly isUSDChecked = this._isUSDChecked.asReadonly();

  public doChange(isUSDChecked: boolean) {
    this._isUSDChecked.set(isUSDChecked);
  }

}
