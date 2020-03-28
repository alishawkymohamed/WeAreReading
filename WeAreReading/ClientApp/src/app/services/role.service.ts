import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as Urls from '../../assets/Config/Config.json';
import { Observable } from 'rxjs';
import { Role } from '../models/Role';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http:HttpClient) { }

  getAll():Observable<Role[]>{
      return this.http.get<Role[]>(`${Urls.prefix}${Urls.roles.getAll}`)
  }

}
