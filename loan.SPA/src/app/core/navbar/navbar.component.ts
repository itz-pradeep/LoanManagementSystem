import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  currentUser$: Observable<IUser>;
  constructor(private accountService: AccountService,private router: Router) { }

  ngOnInit(): void {
   this.currentUser$ = this.accountService.currentUser$;
  }

  logout(){
    this.accountService.logout();
    this.router.navigate(['account']);
  }

}
