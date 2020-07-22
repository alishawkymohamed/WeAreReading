import { Component, OnInit } from "@angular/core";
import { SwaggerClient, BookDTO, AuthTicketDTO } from "src/app/services/SwaggerClient.service";
import { environment } from "src/environments/environment";
import { UserService } from "src/app/services/user.service";
import { ToastrService } from "ngx-toastr";
import { Router } from '@angular/router';

@Component({
  selector: "app-books-list",
  templateUrl: "./books-list.component.html",
  styleUrls: ["./books-list.component.css"],
})
export class BooksListComponent implements OnInit {
  books: BookDTO[];
  recommendedBooks: BookDTO[];
  env: any;
  currentUser: AuthTicketDTO;

  constructor(
    private swagger: SwaggerClient,
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.env = environment;
    this.currentUser = this.userService.CurrentUser;
    this.getUserBooks();
    this.getRecommendedBooks();
  }

  getUserBooks() {
    this.swagger.api_Book_GetAllForUser(undefined, undefined).subscribe((res) => {
      this.books = res;
    });
  }

  getRecommendedBooks() {
    this.swagger.api_Book_GetRecommendedBooks(4).subscribe(res => {
      this.recommendedBooks = res;
    })
  }

  onDelete($event, book: BookDTO) {
    $event.preventDefault();
    this.swagger.api_Book_Delete(book.id).subscribe(
      (res) => {
        console.log(res);
        this.toastr.success("Book deleted successfully");
        this.getUserBooks();
      },
      (error) => {
        console.log(error);
        this.toastr.warning("This book can't be deleted !!");
      }
    );
  }

  onDetailsClick($event, book: BookDTO) {
    $event.preventDefault();
    this.router.navigate(['/books', book.id]);
  }
}
