import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SubscribeSnackBarComponent } from './subscribe-snack-bar.component';

describe('SnackBarComponent', () => {
  let component: SubscribeSnackBarComponent;
  let fixture: ComponentFixture<SubscribeSnackBarComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SubscribeSnackBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubscribeSnackBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
