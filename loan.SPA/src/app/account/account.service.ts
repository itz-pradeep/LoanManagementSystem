import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.baseUrl;
  private currentUserSource = new BehaviorSubject<IUser>(JSON.parse(localStorage.getItem('currentUser$')));
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { 
  }

  IsLoggedIn(){
    return !!localStorage.getItem('token');
  }

  loadCurrentUser(token:string){
    if(token == null){
      this.currentUserSource.next(null);
      return of(null);
    }

    var headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);
    return this.http.get(this.baseUrl + 'account',{headers}).pipe(
      map((user:IUser) => {
        localStorage.setItem('token',user.token);
        localStorage.setItem('currentUser$', JSON.stringify(user));
        this.currentUserSource.next(user);
      })
    );
  } 
 
  login(values: any){
    return this.http.post(this.baseUrl + 'account/login',values).pipe(
      map((user: IUser) => {
        if(user){
          localStorage.setItem('token', user.token);
          localStorage.setItem('currentUser$', JSON.stringify(user));
          this.currentUserSource.next(user);
        } 
      })
    );
  }

  logout(){
    localStorage.removeItem('currentUser$');
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
  }
}
