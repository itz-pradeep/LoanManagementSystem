import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';

const appRoutes: Routes = [
  { path:'',component:LoginComponent}
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
