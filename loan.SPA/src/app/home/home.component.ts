import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ILoanApplication } from '../shared/models/loanApplication';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  searchForm: FormGroup;
  constructor(private homeService: HomeService) { }

  ngOnInit(): void {
    this.setSearchForm();
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
      next: (d) => {
        console.log(d);
      },
      error : (e) => {
        console.log(e);
      }
    });
  }
}


