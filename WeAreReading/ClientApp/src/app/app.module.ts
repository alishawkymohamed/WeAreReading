import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { NgModule } from "@angular/core";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HeaderComponent } from "./layout/header/header.component";
import { FooterComponent } from "./layout/footer/footer.component";
import { HttpClientModule } from "@angular/common/http";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { TokenInterceptor } from "./common/interceptors/token-interceptor";
import { BooksListComponent } from "./books/books-list/books-list.component";
import { AddBookComponent } from "./books/add-book/add-book.component";
import { ToastrModule } from "ngx-toastr";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { LoginComponent } from "./auth/login/login.component";
import { SignupComponent } from "./auth/signup/signup.component";
import { HomeComponent } from "./Home/home.component";
import { GalleryComponent } from "./gallery/gallery.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { BookDetailsComponent } from "./books/book-details/book-details.component";
import { NgbRatingModule, NgbTabsetModule } from "@ng-bootstrap/ng-bootstrap";
import { ProfileComponent } from "./profile/profile.component";
import { RequestsComponent } from "./requests/requests.component";
import { NotAuthorizedComponent } from "./not-authorized/not-authorized.component";
import { NotRespondedReceivedRequestsComponent } from "./requests/not-responded-received-requests/not-responded-received-requests.component";
import { NotRespondedSentRequestsComponent } from "./requests/not-responded-sent-requests/not-responded-sent-requests.component";
import { AcceptedSentRequestsComponent } from "./requests/accepted-sent-requests/accepted-sent-requests.component";
import { RejectedSentRequestsComponent } from "./requests/rejected-sent-requests/rejected-sent-requests.component";
import { AcceptedReceivedRequestsComponent } from "./requests/accepted-received-requests/accepted-received-requests.component";
import { RejectedReceivedRequestsComponent } from "./requests/rejected-received-requests/rejected-received-requests.component";
import { SweetAlert2Module } from "@sweetalert2/ngx-sweetalert2";
import { NgSelectModule } from "@ng-select/ng-select";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    BooksListComponent,
    AddBookComponent,
    LoginComponent,
    SignupComponent,
    HomeComponent,
    GalleryComponent,
    NotFoundComponent,
    BookDetailsComponent,
    ProfileComponent,
    RequestsComponent,
    NotAuthorizedComponent,
    NotRespondedReceivedRequestsComponent,
    NotRespondedSentRequestsComponent,
    AcceptedSentRequestsComponent,
    RejectedSentRequestsComponent,
    AcceptedReceivedRequestsComponent,
    RejectedReceivedRequestsComponent,
  ],
  imports: [
    NgSelectModule,
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbRatingModule,
    SweetAlert2Module.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
    }),
    ReactiveFormsModule,
    BrowserAnimationsModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
