import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  };

  baseUrl = 'api/';

  constructor() { }
}
