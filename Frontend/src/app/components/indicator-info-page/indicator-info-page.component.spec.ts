import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { IndicatorInfoPageComponent } from './indicator-info-page.component';

describe('IndicatorInfoPageComponent', () => {
  let component: IndicatorInfoPageComponent;
  let fixture: ComponentFixture<IndicatorInfoPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ IndicatorInfoPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IndicatorInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
