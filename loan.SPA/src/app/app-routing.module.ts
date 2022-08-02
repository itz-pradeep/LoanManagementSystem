import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'account',loadChildren:() => import('./account/account.module')
  .then(mod => mod.AccountModule),data: {breadcrumb: {skip : true}}},
  {path: 'home',loadChildren:() => import('./home/home.module')
  .then(mod => mod.HomeModule),data: {breadcrumb: {skip : true}}},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
