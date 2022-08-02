import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MatCardModule } from '@angular/material/card';
import { AngularMaterialModule } from '../shared/angular-material.module';



@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    AngularMaterialModule
  ]
})
export class AccountModule { }
