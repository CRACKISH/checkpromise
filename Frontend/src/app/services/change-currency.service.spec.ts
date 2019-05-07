import { TestBed } from '@angular/core/testing';

import { ChangeCurrencyService } from './change-currency.service';

describe('ChangeCurrencyService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ChangeCurrencyService = TestBed.get(ChangeCurrencyService);
    expect(service).toBeTruthy();
  });
});
