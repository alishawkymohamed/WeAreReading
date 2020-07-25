import { Injectable, EventEmitter } from "@angular/core";
import { SwaggerClient } from "./SwaggerClient.service";
import * as jwt_decode from "jwt-decode";
import { Router } from "@angular/router";

@Injectable({ providedIn: "root" })
export class AuthService {
  authEventEmitter = new EventEmitter<boolean>();

  constructor(private swagger: SwaggerClient, private router: Router) {}

  isAuthenticated() {
    const token = localStorage.getItem("access-token");
    if (token) {
      const decryptedToken = jwt_decode(token);
      return new Date(decryptedToken.exp * 1000) > new Date();
    } else {
      return false;
    }
  }

  logout() {
    const refreshToken = localStorage.getItem("refresh-token");
    this.swagger.api_Account_Logout(refreshToken).subscribe((res) => {
      localStorage.clear();
      this.authEventEmitter.next(true);
      this.router.navigate(["/"]);
    });
  }

  getRefreshToken() {
    return localStorage.getItem("refresh-token");
  }
}
