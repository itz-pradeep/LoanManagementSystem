import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from './account/account.service';
import { IUser } from './shared/models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'LMS';
  currentUser$ : Observable<IUser>;
  constructor(private accountService: AccountService,private router: Router){

  }

  ngOnInit(){
    var token = localStorage.getItem("token");
    this.accountService.loadCurrentUser(token).subscribe(
      {
        next: (resp) => {
          console.log("user loaded");
          this.currentUser$ = this.accountService.currentUser$;
        },
        error: (e) => {
          console.log(e);
        }
      }
    );
  }
}
