import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { RouterModule, Routes } from '@angular/router';
import { LoanListComponent } from './loan-list/loan-list.component';
import { LoanCreateComponent } from './loan-create/loan-create.component';

const appRoutes: Routes = [
  { path:'',component:HomeComponent},
  { path:'loan-create',component:LoanCreateComponent}
]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(appRoutes)
  ],
  exports:[
    RouterModule
  ]
})
export class HomeRoutingModule { }
