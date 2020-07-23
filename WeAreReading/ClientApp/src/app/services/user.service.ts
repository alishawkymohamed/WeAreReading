import { Injectable } from "@angular/core";
import { SwaggerClient, AuthTicketDTO } from "./SwaggerClient.service";

@Injectable({ providedIn: "root" })
export class UserService {
  constructor(private swagger: SwaggerClient) {}

  getAuthTicket() {
    this.swagger.api_Account_GetUserAuthTicket().subscribe((res) => {
      localStorage.setItem("auth-ticket", JSON.stringify(res));
    });
  }

  get CurrentUser(): AuthTicketDTO {
    const user = localStorage.getItem("auth-ticket");
    if (user) {
      return JSON.parse(user);
    } else {
      return null;
    }
  }
}
