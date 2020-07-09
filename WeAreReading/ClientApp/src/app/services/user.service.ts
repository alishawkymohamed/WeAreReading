import { Injectable } from "@angular/core";
import * as jwt_decode from "jwt-decode";
import { SwaggerClient } from "./SwaggerClient.service";
import { CookieService } from "ngx-cookie-service";

@Injectable({ providedIn: "root" })
export class UserService {
  private user: any;

  constructor(private swagger: SwaggerClient, private cookie: CookieService) {}

  getCurrentUser() {
    // this.swagger.api_Account_GetUserAuthTicket().subscribe((res) => {
    //   console.log(res);
    // });
    const token = this.cookie.get("access-token");
    if (token) {
      this.user = jwt_decode(token);
      return this.user;
    }
  }
}
