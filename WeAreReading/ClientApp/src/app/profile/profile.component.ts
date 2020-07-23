import { Component, OnInit } from "@angular/core";
import { UserService } from "../services/user.service";
import {
  AuthTicketDTO,
  SwaggerClient,
  UserDTO,
} from "../services/SwaggerClient.service";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.css"],
})
export class ProfileComponent implements OnInit {
  currentUser: AuthTicketDTO;
  profileData: UserDTO;
  env: any;

  constructor(
    private userService: UserService,
    private swagger: SwaggerClient
  ) {}

  ngOnInit(): void {
    this.env = environment;
    this.currentUser = this.userService.CurrentUser;
    this.getProfileDate();
  }

  getProfileDate() {
    this.swagger
      .api_Account_GetUserDetails(this.currentUser.userId)
      .subscribe((res) => {
        this.profileData = res;
      });
  }
}
