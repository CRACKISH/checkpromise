import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DonateDialogComponent } from './donate-dialog.component';

describe('DonateDialogComponent', () => {
  let component: DonateDialogComponent;
  let fixture: ComponentFixture<DonateDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DonateDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DonateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
