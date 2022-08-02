import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  
  constructor(private router: Router,private accountService: AccountService) {
    this.loginForm = new FormGroup({
      username: new FormControl(null,[Validators.required,Validators.email]),
      password: new FormControl(null,Validators.required)
    });
   }

  get username() : AbstractControl{
    return this.loginForm.get('username')!;
  }

  get password() : AbstractControl{
    return this.loginForm.get('password')!;
  }

  ngOnInit(): void {
   
  }

  onSubmit(){
    if(this.loginForm.valid){
      this.accountService.login(this.loginForm.value).subscribe(
        () => {
          this.router.navigateByUrl('/home');
        }
      );
    }
    else{
      return;
    }
  }

}
