import { provideZonelessChangeDetection } from '@angular/core';
import { TestBed } from '@angular/core/testing';

import { ChangeCurrencyService } from './change-currency.service';

describe('ChangeCurrencyService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [provideZonelessChangeDetection()]
  }));

  it('should be created', () => {
    const service: ChangeCurrencyService = TestBed.inject(ChangeCurrencyService);
    expect(service).toBeTruthy();
  });
});
