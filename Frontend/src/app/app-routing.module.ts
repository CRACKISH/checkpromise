import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ChartInfoPageComponent } from './components/chart-info-page/chart-info-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';

const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: ':id', component: ChartInfoPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
