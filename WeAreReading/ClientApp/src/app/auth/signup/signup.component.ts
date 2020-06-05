import { Component, OnInit } from "@angular/core";
import { SwaggerClient } from "src/app/services/SwaggerClient.service";

@Component({
  selector: "app-signup",
  templateUrl: "./signup.component.html",
  styleUrls: ["./signup.component.css"],
})
export class SignupComponent implements OnInit {
  constructor(private swagger: SwaggerClient) {}

  ngOnInit(): void {
    this.swagger.api_Government_GetAll().subscribe((res) => {
      console.log(res);
    });
  }
}
