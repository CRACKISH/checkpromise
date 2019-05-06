import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatDialogModule, MatDividerModule, MatSlideToggleModule, MatSnackBarModule } from '@angular/material';
import { ChartsModule } from 'ng2-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ChartComponent } from './components/chart/chart.component';
import { CheckboxComponent } from './components/checkbox/checkbox.component';
import { DonateDialogComponent } from './components/donate-dialog/donate-dialog.component';
import { ChartInfoPageComponent } from './components/chart-info-page/chart-info-page.component';
import { SubscribeSnackBarComponent } from './components/subscribe-snack-bar/subscribe-snack-bar.component';
import { MainPageComponent } from './components/main-page/main-page.component';


@NgModule({
  declarations: [
    AppComponent,
    ChartComponent,
    CheckboxComponent,
    DonateDialogComponent,
    ChartInfoPageComponent,
    SubscribeSnackBarComponent,
    MainPageComponent
  ],
  entryComponents: [
    DonateDialogComponent,
    SubscribeSnackBarComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    ChartsModule,
    HttpClientModule,
    MatButtonModule,
    MatDialogModule,
    MatDividerModule,
    MatSlideToggleModule,
    MatSnackBarModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
