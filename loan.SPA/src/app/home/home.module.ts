import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { SharedModule } from '../shared/shared.module';
import { LoanListComponent } from './loan-list/loan-list.component';
import { LoanCreateComponent } from './loan-create/loan-create.component';

@NgModule({
  declarations: [
    HomeComponent,
    LoanListComponent,
    LoanCreateComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    SharedModule
  ]
})
export class HomeModule { }
