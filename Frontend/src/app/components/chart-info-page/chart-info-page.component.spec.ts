import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChartInfoPageComponent } from './chart-info-page.component';

describe('ChartInfoPageComponent', () => {
  let component: ChartInfoPageComponent;
  let fixture: ComponentFixture<ChartInfoPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChartInfoPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChartInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
