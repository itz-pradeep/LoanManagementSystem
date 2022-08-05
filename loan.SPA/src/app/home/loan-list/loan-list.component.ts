import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { AccountService } from 'src/app/account/account.service';
import { ILoanApplication } from 'src/app/shared/models/loanApplication';
import { IUser } from 'src/app/shared/models/user';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.component.html',
  styleUrls: ['./loan-list.component.css']
})
export class LoanListComponent implements OnInit {
  loanApplications: ILoanApplication[] = [];
  currentUser$: Observable<IUser>;
  constructor(private homeService: HomeService,private accountService:AccountService) { }


  ngOnInit(): void {
    this.setApplicationGrid();
    this.currentUser$ = this.accountService.currentUser$;
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
    const result = confirm("Are you sure you want to cancel this loan application?");
    if(result && result === true){
      this.homeService.cancelLoanApplication(loanId).subscribe({
        next: () => {
          let index = this.loanApplications.findIndex((obj) => {
            return obj.id == loanId;
          });
          this.loanApplications.splice(index,1);
          this.homeService.loanApplicationsFetched.next(this.loanApplications);
        }
      });
    }
  
  }


}
