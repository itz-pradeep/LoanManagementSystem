import { Component, OnInit } from '@angular/core';
import { ILoanApplication } from 'src/app/shared/models/loanApplication';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.component.html',
  styleUrls: ['./loan-list.component.css']
})
export class LoanListComponent implements OnInit {
  loanApplications: ILoanApplication[] = [];
  constructor(private homeService: HomeService) { }


  ngOnInit(): void {
    this.setApplicationGrid();
   
  }

  setApplicationGrid(){
    this.homeService.loanApplicationsFetched.subscribe({
      next : (data) => {
        this.loanApplications = data
      },
      error: (e) => {
        console.log(e)
      }
    });
  }

  cancel(loanId: number){
    alert(loanId);
    this.homeService.cancelLoanApplication(loanId).subscribe({
      next: () => {
        this.setApplicationGrid();
      }
    });
  }


}
