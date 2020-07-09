import { Component, OnInit } from "@angular/core";
import {
  SwaggerClient,
  UserLoginDTO,
} from "src/app/services/SwaggerClient.service";
import { ToastrService } from "ngx-toastr";
import { CookieService } from "ngx-cookie-service";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  constructor(
    private swagger: SwaggerClient,
    private toastr: ToastrService,
    private cookie: CookieService,
    private router: Router
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
          this.cookie.set("access-token", res.access_token);
          this.router.navigate(["/"]);
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
