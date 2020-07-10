import { Component, OnInit } from "@angular/core";
import {
  SwaggerClient,
  RoleDTO,
  GovernmentDTO,
  RegisterUserDTO,
} from "src/app/services/SwaggerClient.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-signup",
  templateUrl: "./signup.component.html",
  styleUrls: ["./signup.component.css"],
})
export class SignupComponent implements OnInit {
  roles: RoleDTO[];
  govs: GovernmentDTO[];
  env: any;
  fileExtenstion: string;
  userImageId: string;
  registerForm: FormGroup;
  long: string;
  lat: string;

  constructor(
    private swagger: SwaggerClient,
    private http: HttpClient,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.env = environment;
    this.getRoles();
    this.getGovernments();
    this.setupForm();
    this.getUserLocation();
  }

  setupForm() {
    this.registerForm = this.fb.group({
      name: ["", Validators.required],
      username: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      phone: ["", [Validators.required, Validators.pattern("(01)[0-9]{9}")]],
      password: ["", Validators.required],
      confirmPassword: ["", Validators.required],
      roleId: ["1", Validators.required],
      governmentId: ["1", Validators.required],
      accepted: [false],
    });
  }

  getRoles() {
    this.swagger.api_Role_GetAll().subscribe((res) => {
      this.roles = res;
    });
  }

  getGovernments() {
    this.swagger.api_Government_GetAll().subscribe((res) => {
      this.govs = res;
    });
  }

  getUserLocation() {
    navigator.geolocation.getCurrentPosition((res) => {
      this.long = res.coords.longitude.toString();
      this.lat = res.coords.latitude.toString();
    });
  }

  onFileUpload(files: FileList) {
    const formData: FormData = new FormData();
    formData.append("Photo", files[0], files[0].name);

    const headers = new HttpHeaders();
    headers.append("Accept", "application/json");
    const options = { headers: headers };

    this.http
      .post(
        environment.baseUrl + "api/Account/UploadUserImage",
        formData,
        options
      )
      .subscribe((res: any) => {
        this.userImageId = res.imageId;
        this.fileExtenstion = "." + files[0].name.split(".").pop();
      });
  }

  onSubmit() {
    const accepted = this.registerForm.get("accepted").value;
    if (!accepted) {
      this.toastr.warning("Please accept terms and conditions !!");
    }

    if (!this.userImageId && +this.registerForm.get("roleId").value === 1) {
      this.toastr.warning("Please upload your photo !!");
    }

    if (this.registerForm.valid) {
      this.swagger.api_Account_Register({
        confirmPassword: this.registerForm.get("confirmPassword").value,
        email: this.registerForm.get("email").value,
        fullName: this.registerForm.get("name").value,
        governmentId: +this.registerForm.get("governmentId").value,
        latitude: this.lat,
        longitude: this.long,
        password: this.registerForm.get("password").value,
        phoneNumber: this.registerForm.get("phone").value,
        profilePictureId: `${this.userImageId}.${this.fileExtenstion}`,
        roleId: +this.registerForm.get("roleId").value,
        username: this.registerForm.get("username").value
      } as RegisterUserDTO).subscribe(res => {
        this.toastr.success("User successfully registered ...");
      }, error => {
        if (error.status === 400) {
          const validations = JSON.parse(error.response);
          Object.keys(validations).forEach(x => {
            this.toastr.error(validations[x].join('/n'));
          });
        }
      })
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
