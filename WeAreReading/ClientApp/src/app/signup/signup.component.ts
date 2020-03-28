import { Component, OnInit } from "@angular/core";
import { SignUp } from "../models/SignUp";
import {
  FormGroup,
  FormControl,
  Validators,
  AbstractControl,
  ValidationErrors
} from "@angular/forms";
import { RoleService } from "../services/role.service";
import { Role } from "../models/Role";
import { LocationService } from "../services/location.Service";
import { Router } from "@angular/router";
import { SignUpService } from "../services/signup.service";

@Component({
  selector: "app-signup",
  templateUrl: "./signup.component.html",
  styleUrls: ["./signup.component.scss"]
})
export class SignupComponent implements OnInit {
  data = new SignUp();
  form: FormGroup;
  roles: Role[];
  constructor(
    private role: RoleService,
    private locationService: LocationService,
    private router:Router,
    private service:SignUpService
  ) {}

  ngOnInit() {
    this.form = new FormGroup(
      {
        firstName: new FormControl(this.data.firstName, [Validators.required]),
        lastName: new FormControl(this.data.lastName, [
          Validators.required,
          Validators.minLength(3)
        ]),
        username: new FormControl(this.data.username, [
          Validators.required,
          Validators.minLength(3)
        ]),
        email: new FormControl(this.data.email, [
          Validators.required,
          Validators.pattern(`^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$`)
        ]),
        phoneNumber: new FormControl(this.data.phoneNumber, [
          Validators.required,
          Validators.minLength(10)
        ]),
        password: new FormControl(this.data.password, [
          Validators.required,
          Validators.minLength(8)
        ]),
        confirmPassword: new FormControl(this.data.confirmPassword, [
          Validators.required,
          Validators.minLength(8)
        ]),
        roleId: new FormControl(this.data.roleId, [Validators.required])
      },
      [this.formValidator]
    );
    this.getRoles();
  }
  focus($event) {
    $event.target.parentElement.parentElement.classList.add("focused");
  }
  blur($event) {
    $event.target.parentElement.parentElement.classList.remove("focused");
  }

  getRoles() {
    this.role.getAll().subscribe(data => {
      this.roles = data;
    });
  }

  formValidator(form: AbstractControl): ValidationErrors {
    debugger;
    let password = form.get("password").value;
    let confirmpassword = form.get("confirmPassword").value;
    if (password != confirmpassword) {
      form.get("confirmPassword").setErrors({ confirmPassword: true });
    } else {
      form.get("confirmPassword").setErrors(null);
    }
    return;
  }

  onSubmit() {
    if (this.form.valid) {
      this.data = this.form.value as SignUp;
      this.locationService.getPosition().then(pos => {
        this.data.latitude = pos.lat;
        this.data.longitude = pos.lng;
      });
      this.data.roleId = +this.data.roleId;
      this.service.save(this.data).subscribe(()=>{
        this.router.navigateByUrl('/login');
      },
      ()=>{

      })
    } else {
      this.form.markAsDirty();
    }
  }
}
