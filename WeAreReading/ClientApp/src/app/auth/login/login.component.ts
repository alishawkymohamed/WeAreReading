import { Component, OnInit } from "@angular/core";
import {
  SwaggerClient,
  UserLoginDTO,
} from "src/app/services/SwaggerClient.service";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import { UserService } from "src/app/services/user.service";
import { AuthService } from "src/app/services/auth.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  constructor(
    private swagger: SwaggerClient,
    private toastr: ToastrService,
    private router: Router,
    private authService: AuthService,
    private userService: UserService
  ) {}

  ngOnInit() {}

  login(email, password) {
    if (!email.value) {
      this.toastr.error("Please enter a valid email of username !!");
      return;
    }

    if (!password.value) {
      this.toastr.error("Please enter a valid password !!");
      return;
    }

    this.swagger
      .api_Account_Login({
        username: email.value,
        password: password.value,
      } as UserLoginDTO)
      .subscribe(
        (res) => {
          localStorage.setItem("access-token", res.access_token);
          localStorage.setItem("refresh-token", res.refresh_token);
          this.userService.getAuthTicket();
          this.authService.authEventEmitter.next(true);
          this.router.navigate(["/gallery"]);
        },
        (error) => {
          if (error.status === 401) {
            this.toastr.error("Invalid Credentials !!");
          } else {
            this.toastr.error("Please try again later !!");
          }
        }
      );
  }
}
