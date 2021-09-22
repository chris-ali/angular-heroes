import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor() { }

  getCurrentUser(): User {
    return { id: 1, userName: 'chris', firstName: 'Chris', lastName: 'Ali', email: 'chris@test.com' };
  } 
}
