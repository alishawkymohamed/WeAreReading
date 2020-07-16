import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SignupComponent } from "./auth/signup/signup.component";
import { LoginComponent } from "./auth/login/login.component";
import { BooksListComponent } from './books/books-list/books-list.component';
import { AddBookComponent } from './books/add-book/add-book.component';
import { HomeComponent } from './Home/home.component';
import { GalleryComponent } from './gallery/gallery.component';
import { NotFoundComponent } from './not-found/not-found.component';

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
    path: "**",
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
