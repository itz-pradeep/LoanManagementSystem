import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Subject } from 'rxjs';
import { first, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ILoanApplication } from '../shared/models/loanApplication';
import { ILoanType } from '../shared/models/loanType';
import { BaseService } from '../shared/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService extends BaseService {
  baseUrl = environment.baseUrl;
  loanApplicationsFetched  = new Subject<ILoanApplication[]>();
  constructor(private http: HttpClient) { 
    super();
  }

  getLoanTypes(){
    var headers = this.getRequestHeaders();
    return this.http.get<ILoanType[]>(this.baseUrl + "Loan/loanTypes",
    {headers});
  }

  createLoanApplication(value: any){
    var headers = this.getRequestHeaders();
    return this.http.post(this.baseUrl + "Loan",value,{headers});
  }

  updateLoanApplication(loanNumber: number,value: any){
    var headers = this.getRequestHeaders();
    return this.http.put(this.baseUrl + 'Loan/' + loanNumber,value,{headers});
  }

  getLoanApplications(){
    var headers = this.getRequestHeaders();
    return this.http.get<ILoanApplication[]>(this.baseUrl + "Loan",
    {headers});
  }

  getLoanApplicationById(loanNumber: number){
    var headers = this.getRequestHeaders();
    
    return this.http.get<ILoanApplication>(this.baseUrl + 'Loan/' + loanNumber ,
    {headers});
  }

  getLoanApplicationsByFilter(firstName?:string,lastName?:string,loanNumber?:number){
    var params = new HttpParams();
    if(firstName !== null){
      params = params.append('firstName',firstName);
    }

    if(lastName !== null){
      params = params.append('lastName',lastName);
    }

    if(loanNumber !== null){
      params = params.append('loanId',loanNumber);
    }
    
    var headers = this.getRequestHeaders();
    return this.http.get<ILoanApplication[]>(this.baseUrl + "Loan",
    {headers,params}).pipe(
      map((lp:ILoanApplication[]) => {
        this.loanApplicationsFetched.next(lp);
      })
    );
  }

  cancelLoanApplication(laonNumber: number){
    var headers = this.getRequestHeaders();
    return this.http.delete(this.baseUrl+"Loan/"+laonNumber,{headers});
  }
}
