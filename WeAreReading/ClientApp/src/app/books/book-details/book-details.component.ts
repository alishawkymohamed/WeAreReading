import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import {
  SwaggerClient,
  BookDTO,
  UserDTO,
  CreateRequestDTO,
} from "src/app/services/SwaggerClient.service";
import { environment } from "src/environments/environment";
import { UserService } from "src/app/services/user.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-book-details",
  templateUrl: "./book-details.component.html",
  styleUrls: ["./book-details.component.css"],
})
export class BookDetailsComponent implements OnInit {
  bookId: string;
  book: BookDTO;
  env: any;
  ownerDto: UserDTO;
  addBooks: BookDTO[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private swagger: SwaggerClient,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.env = environment;
    this.getBookId();
  }

  getBookId() {
    this.route.params.subscribe((params) => {
      this.bookId = params["id"];
      if (!this.bookId || !Number.isInteger(parseInt(this.bookId))) {
        this.router.navigate(["/not-found"]);
      } else {
        this.getBookDetails();
      }
    });
  }

  getBookDetails() {
    this.swagger.api_Book_GetDetails(+this.bookId).subscribe((res) => {
      this.book = res;
      this.getOwnerDetails(this.book.ownerId);
      this.getAdditionalBooks(this.book.ownerId);
    });
  }

  getOwnerDetails(userId: number) {
    this.swagger.api_Account_GetUserDetails(userId).subscribe((res) => {
      this.ownerDto = res;
    });
  }

  getAdditionalBooks(userId: number) {
    this.swagger
      .api_Book_GetAllForUser(userId, 3, undefined)
      .subscribe((res) => {
        this.addBooks = res;
      });
  }

  OnBorrow() {
    this.swagger
      .api_Request_SendRequest({
        bookId: +this.bookId,
        receiverId: this.book.ownerId,
        senderId: this.userService.CurrentUser.userId,
      } as CreateRequestDTO)
      .subscribe(
        (res) => {
          this.toastr.success("Your request sent successfully.");
        },
        (error) => {
          if (error.response === "SentBefore") {
            this.toastr.warning(
              "You sent a request to borrow this book before !!"
            );
          } else {
            this.toastr.error("Error occured while processing your request !!");
          }
        }
      );
  }
}
