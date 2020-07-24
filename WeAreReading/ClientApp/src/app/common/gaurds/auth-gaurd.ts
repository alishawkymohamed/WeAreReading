import { Injectable } from "@angular/core";
import {
  Router,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { AuthService } from "src/app/services/auth.service";

@Injectable({ providedIn: "root" })
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const isAuthentcated = this.authService.isAuthenticated();
    if (isAuthentcated) {
      // logged in so return true
      return true;
    } else {
      // not logged in so redirect to login page with the return url
      localStorage.setItem("returnUrl", state.url);
      this.router.navigate(["/not-authorized"]);
      return false;
    }
  }
}
