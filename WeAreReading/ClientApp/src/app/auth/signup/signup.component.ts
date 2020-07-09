import { Component, OnInit } from "@angular/core";
import {
  SwaggerClient,
  RoleDTO,
  GovernmentDTO,
} from "src/app/services/SwaggerClient.service";

@Component({
  selector: "app-signup",
  templateUrl: "./signup.component.html",
  styleUrls: ["./signup.component.css"],
})
export class SignupComponent implements OnInit {
  roles: RoleDTO[];
  govs: GovernmentDTO[];

  constructor(private swagger: SwaggerClient) {}

  ngOnInit(): void {
    this.getRoles();
    this.getGovernments();
  }

  getRoles() {
    this.swagger.api_Role_GetAll().subscribe((res) => {
      this.roles = res;
    });
  }

  getGovernments() {
    this.swagger.api_Government_GetAll().subscribe((res) => {
      this.govs = res;
    });
  }
}
