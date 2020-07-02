import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from "@angular/common/http";
import { Observable } from "rxjs";
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  private token =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwOWQyN2VhNy0zNzA3LTQ5ZjAtODVkMS0xNzdmYzVmNjRhNGEiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDM1NCIsImlhdCI6MTU5MzY1Nzg3MTg5OCwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiIxIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFsaXNoYXdreSIsIkRpc3BsYXlOYW1lIjoiQWxpIFNoYXdreSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvc2VyaWFsbnVtYmVyIjoiNDBhZjcwMDYtNjIxYy00MjhkLTkxZTEtNGUwZWVlM2E3ZGM1IiwibmJmIjoxNTkzNjU3ODcxLCJleHAiOjE1OTM2NjE0NzEsImF1ZCI6IkFueSJ9.rA-LsOvxINyUveSick6Fc8_nwwQy7342BCga1u7Rqco";
  constructor() {}
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.token}`,
      },
    });
    return next.handle(request);
  }
}
