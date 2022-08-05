import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router,private toastr: ToastrService,private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if(error){
          if(error.status == 400){
            this.toastr.show(error.error.message,error.error.statusCode);
          }
          if(error.status == 401){
            this.toastr.show(error.error.message,error.error.statusCode);
            this.accountService.logout();
          }
          if(error.status == 404){
            this.router.navigateByUrl('/not-found');
          }
          if(error.status == 500){
            this.router.navigateByUrl('/server-error');
          }
        }
        return throwError(() => new Error(error.message));
      })
    );
  }
}
