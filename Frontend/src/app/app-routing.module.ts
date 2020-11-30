import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { IndicatorInfoPageComponent } from './components/indicator-info-page/indicator-info-page.component';
import { MainPageComponent } from './components/main-page/main-page.component';

const routes: Routes = [
  { path: '', component: MainPageComponent },
  { path: ':id', component: IndicatorInfoPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
