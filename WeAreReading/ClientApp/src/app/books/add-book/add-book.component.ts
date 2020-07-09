import { Component, OnInit } from "@angular/core";
import {
  SwaggerClient,
  CategoryDTO,
  InsertBookDTO,
  StatusDTO,
} from "src/app/services/SwaggerClient.service";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { Router } from "@angular/router";

@Component({
  selector: "app-add-book",
  templateUrl: "./add-book.component.html",
  styleUrls: ["./add-book.component.css"],
})
export class AddBookComponent implements OnInit {
  categories: CategoryDTO[];
  statuses: StatusDTO[];
  bookCoverId: string;
  fileExtenstion: string;
  addBookForm: FormGroup;
  env: any;

  constructor(
    private swagger: SwaggerClient,
    private http: HttpClient,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.env = environment;
    this.getAllCategories();
    this.getAllStatuses();
    this.setupForm();
  }

  setupForm() {
    this.addBookForm = this.fb.group({
      title: ["", Validators.required],
      author: ["", Validators.required],
      description: ["", Validators.required],
      rating: [""],
      price: [""],
      copiesCount: ["", Validators.required],
      category: [1, Validators.required],
      status: [1, Validators.required],
    });
  }

  getAllCategories() {
    this.swagger.api_Category_GetAll().subscribe((res) => {
      this.categories = res;
    });
  }

  getAllStatuses() {
    this.swagger.api_Status_GetAll().subscribe((res) => {
      this.statuses = res;
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
        environment.baseUrl + "api/Book/UploadBookCoverImage",
        formData,
        options
      )
      .subscribe((res: any) => {
        this.bookCoverId = res.imageId;
        this.fileExtenstion = "." + files[0].name.split(".").pop();
      });
  }

  onSubmit() {
    if (!this.addBookForm.valid) {
      this.toastr.error("Please Enter Valid Data !!");
      return;
    }

    if (!this.bookCoverId) {
      this.toastr.error("Please Upload Book Cover !!");
      return;
    }

    if (this.bookCoverId && this.addBookForm.valid) {
      this.swagger
        .api_Book_Insert({
          author: this.addBookForm.get("author").value,
          categoryId: +this.addBookForm.get("category").value,
          copiesCount: this.addBookForm.get("copiesCount").value,
          coverPhotoId: this.bookCoverId + this.fileExtenstion,
          description: this.addBookForm.get("description").value,
          price: this.addBookForm.get("price").value,
          rating: this.addBookForm.get("rating").value,
          title: this.addBookForm.get("title").value,
          statusId: +this.addBookForm.get("status").value,
        } as InsertBookDTO)
        .subscribe((res) => {
          this.toastr.success("Book Added Successfully");
          setTimeout(() => {
            this.router.navigate(["/books/list"]);
          }, 1000);
        });
    }
  }
}
