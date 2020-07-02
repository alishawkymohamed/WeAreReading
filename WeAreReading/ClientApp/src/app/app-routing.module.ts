import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SignupComponent } from "./auth/signup/signup.component";
import { LoginComponent } from "./auth/login/login.component";
import { BooksListComponent } from './books/books-list/books-list.component';
import { AddBookComponent } from './books/add-book/add-book.component';

const routes: Routes = [
  {
    path: "",
    component: SignupComponent,
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
    path: "books/list",
    component: BooksListComponent,
  },
  {
    path: "books/add",
    component: AddBookComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
