import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { ILoanApplication } from 'src/app/shared/models/loanApplication';
import { ILoanType } from 'src/app/shared/models/loanType';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-loan-create',
  templateUrl: './loan-create.component.html',
  styleUrls: ['./loan-create.component.css']
})
export class LoanCreateComponent implements OnInit {
  loanTypes: ILoanType[] = [];
  createForm:FormGroup;
  loanApplication: ILoanApplication;
  editMode: boolean = false;
  loanId: number;
  pageTitle: string = 'Create Application'
  constructor(private homeService: HomeService
    ,private route: ActivatedRoute
    ,private router: Router) { }

  ngOnInit(): void {
     this.route.params.subscribe({
      next:(params: Params) => {
        this.loanId = params['id'];
        this.editMode = params['id'] != null;
      }
     });

     this.initForm();
     this.getLoanTypes();
  }

  initForm(){
    this.setLoanForm();

    if(this.editMode){

      this.pageTitle = 'Edit Application';

      this.homeService.getLoanApplicationById(this.loanId).subscribe({
        next: (data: ILoanApplication) =>{
         this.createForm.patchValue(data);
        }
      });

    }

  }

  setLoanForm(){
    this.createForm = new FormGroup({
      firstName: new FormControl(null,[Validators.required]),
      lastName: new FormControl(null,[Validators.required]),
      loanAmount: new FormControl(null,[Validators.required,Validators.min(10000)
      ,Validators.max(1000000)]),
      loanTenure: new FormControl(null,[Validators.required,
        Validators.min(3),Validators.max(50)]),
      propertyAddress: new FormControl(null,[Validators.required]),
      loanTypeId: new FormControl(-1,[Validators.required])
    });
  }

  get firstName() : AbstractControl{
    return this.createForm.get('firstName')!;
  }

  get lastName() : AbstractControl{
    return this.createForm.get('lastName')!;
  }
  get loanAmount() : AbstractControl{
    return this.createForm.get('loanAmount')!;
  }
  get loanTenure() : AbstractControl{
    return this.createForm.get('loanTenure')!;
  }
  get propertyAddress() : AbstractControl{
    return this.createForm.get('propertyAddress')!;
  }
  get loanTypeId() : AbstractControl{
    return this.createForm.get('loanTypeId')!;
  }

  getLoanTypes(){
    this.homeService.getLoanTypes().subscribe({
      next: (d) => {
        this.loanTypes = d
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  OnSubmit(){
    if(this.createForm.invalid){
      return;
    }

    if(this.editMode){
      this.homeService.updateLoanApplication(this.loanId,this.createForm.value).subscribe({
        next: () => {
          this.router.navigateByUrl('home');
        },
        error: (e) => {
          console.log(e);
        } 
      });;
    }
    else{
      this.homeService.createLoanApplication(this.createForm.value).subscribe({
        next: () => {
          this.router.navigateByUrl('home');
        },
        error: (e) => {
          console.log(e);
        } 
      });
    }

   
  }

  OnReset(){
    this.createForm.reset();
    this.loanTypeId.setValue(-1);
  }

}
