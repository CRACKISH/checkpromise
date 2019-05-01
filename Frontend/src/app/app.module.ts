import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatDialogModule, MatDividerModule, MatSlideToggleModule } from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ChartComponent } from './components/chart/chart.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';
import { ChartInfoPageComponent } from './components/chart-info-page/chart-info-page.component';


@NgModule({
  declarations: [
    AppComponent,
    ChartComponent,
    CheckboxComponent,
    DonateDialogComponent,
    ChartInfoPageComponent
  ],
  entryComponents: [
    DonateDialogComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatButtonModule,
    MatDialogModule,
    MatDividerModule,
    MatSlideToggleModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
