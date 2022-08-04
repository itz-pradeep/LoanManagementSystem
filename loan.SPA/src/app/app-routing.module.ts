import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { AuthGuard } from './shared/services/auth.guard';

const routes: Routes = [
  {path:'',pathMatch: 'full',redirectTo: 'account'},
  {path:'not-found',component: NotFoundComponent},
  {path:'server-error',component: ServerErrorComponent},
  {path: 'account',loadChildren:() => import('./account/account.module')
  .then(mod => mod.AccountModule),data: {breadcrumb: {skip : true}}},
  {path: 'home',loadChildren:() => import('./home/home.module')
  .then(mod => mod.HomeModule),data: {breadcrumb: {skip : true}},canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
