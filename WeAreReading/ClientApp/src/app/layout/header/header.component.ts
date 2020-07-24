import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { SwaggerClient } from "src/app/services/SwaggerClient.service";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.css"],
})
export class HeaderComponent implements OnInit {
  isAuthenticated: boolean;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.checkAuth();
    this.authService.authEventEmitter.subscribe((res) => {
      this.checkAuth();
    });
  }

  checkAuth() {
    this.isAuthenticated = this.authService.isAuthenticated();
  }

  logout($event) {
    $event.preventDefault();
    this.authService.logout();
  }
}
