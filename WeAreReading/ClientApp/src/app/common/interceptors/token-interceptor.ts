import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable } from "rxjs";
import { CookieService } from "ngx-cookie-service";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private token;

  constructor(
    private toastr: ToastrService,
    private cookie: CookieService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.token = this.cookie.get("access-token");
    if (this.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.token}`,
        },
      });
    }
    return next.handle(request);
  }
}
