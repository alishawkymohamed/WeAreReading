import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }
  authintecated(){
    return localStorage.getItem('authToken') != null;
  }
}
