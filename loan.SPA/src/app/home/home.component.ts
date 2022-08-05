import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ILoanApplication } from '../shared/models/loanApplication';
import { HomeService } from './home.service';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { IUser } from '../shared/models/user';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchForm: FormGroup;
  currentUser$: Observable<IUser>;
  constructor(private homeService: HomeService
    ,private accountService:AccountService
    ,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.setSearchForm();
    this.currentUser$ = this.accountService.currentUser$;
  }

  setSearchForm() {
    this.searchForm = new FormGroup({
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      loanNumber: new FormControl(null)
    });
  }
  
  searchApplications(){
    const firstName = this.searchForm.value.firstName;
    const lastName = this.searchForm.value.lastName;
    const loanNumber = this.searchForm.value.loanNumber;
    this.homeService.getLoanApplicationsByFilter(firstName,lastName,loanNumber)
    .subscribe({
      next: (lp:ILoanApplication[]) => {
        if(lp.length == 0){
          this.toastr.show("Zero applications found with given criteria.");
        }
        if(lp.length > 1){
          this.toastr.show("Found multiple applications. Use other search fields to narrow down the results.");
        }
        else{
          this.homeService.loanApplicationsFetched.next(lp);
        }
       
      },
      error : (e) => {
        console.log(e);
      }
    });
  }
}


