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
    return this.http.get<ILoanType[]>(this.baseUrl + "Loan/loanTypes");
  }

  createLoanApplication(value: any){
    return this.http.post(this.baseUrl + "Loan",value);
  }

  updateLoanApplication(loanNumber: number,value: any){
    return this.http.put(this.baseUrl + 'Loan/' + loanNumber,value);
  }

  getLoanApplications(){
    return this.http.get<ILoanApplication[]>(this.baseUrl + "Loan");
  }

  getLoanApplicationById(loanNumber: number){
    return this.http.get<ILoanApplication>(this.baseUrl + 'Loan/' + loanNumber);
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
    
    return this.http.get<ILoanApplication[]>(this.baseUrl + "Loan",
    {params});
  }

  cancelLoanApplication(laonNumber: number){
    return this.http.delete(this.baseUrl+"Loan/"+laonNumber);
  }
}
