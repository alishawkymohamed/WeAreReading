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
import { HomeComponent } from './Home/home.component';
import { GalleryComponent } from './gallery/gallery.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { BookDetailsComponent } from './books/book-details/book-details.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
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
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    NgbRatingModule,
    ToastrModule.forRoot({
      timeOut: 5000,
    }),
    ReactiveFormsModule,
    BrowserAnimationsModule
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
export class AppModule { }
