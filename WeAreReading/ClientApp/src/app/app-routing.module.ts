import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SignupComponent } from "./auth/signup/signup.component";
import { LoginComponent } from "./auth/login/login.component";
import { BooksListComponent } from "./books/books-list/books-list.component";
import { AddBookComponent } from "./books/add-book/add-book.component";
import { HomeComponent } from "./Home/home.component";
import { GalleryComponent } from "./gallery/gallery.component";
import { NotFoundComponent } from "./not-found/not-found.component";
import { BookDetailsComponent } from "./books/book-details/book-details.component";
import { ProfileComponent } from "./profile/profile.component";
import { RequestsComponent } from "./requests/requests.component";
import { NotAuthorizedComponent } from "./not-authorized/not-authorized.component";

const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },
  {
    path: "auth/signup",
    component: SignupComponent,
  },
  {
    path: "auth/login",
    component: LoginComponent,
  },
  {
    path: "profile",
    component: ProfileComponent,
  },
  {
    path: "requests",
    component: RequestsComponent,
  },
  {
    path: "gallery",
    component: GalleryComponent,
  },
  {
    path: "books/list",
    component: BooksListComponent,
  },
  {
    path: "books/add",
    component: AddBookComponent,
  },
  {
    path: "books/:id",
    component: BookDetailsComponent,
  },
  {
    path: "not-authorized",
    component: NotAuthorizedComponent,
  },
  {
    path: "not-found",
    component: NotFoundComponent,
  },
  {
    path: "**",
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
