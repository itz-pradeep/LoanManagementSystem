import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor() { }

  getRequestHeaders(){
    var token = localStorage.getItem("token");
    var headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${token}`);

    return headers;
  }
}
