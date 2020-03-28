import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import * as Urls from "../../assets/Config/Config.json";
import { SignUp } from "../models/SignUp";

@Injectable({
  providedIn: "root"
})
export class SignUpService {
  constructor(private http: HttpClient) {}

  save(data: SignUp) {
    return this.http.post(`${Urls.prefix}${Urls.account.register}`, data);
  }
}
