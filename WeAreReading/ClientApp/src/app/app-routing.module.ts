import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SignupComponent } from "./auth/signup/signup.component";
import { LoginComponent } from "./auth/login/login.component";

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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
