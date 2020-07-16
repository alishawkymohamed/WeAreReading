import { Component, OnInit } from "@angular/core";
import { SwaggerClient, BookDTO, AuthTicketDTO } from "src/app/services/SwaggerClient.service";
import { environment } from "src/environments/environment";
import { UserService } from "src/app/services/user.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-books-list",
  templateUrl: "./books-list.component.html",
  styleUrls: ["./books-list.component.css"],
})
export class BooksListComponent implements OnInit {
  books: BookDTO[];
  env: any;
  currentUser: AuthTicketDTO;

  constructor(
    private swagger: SwaggerClient,
    private userService: UserService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.env = environment;
    this.getUserBooks();
    this.currentUser = this.userService.CurrentUser;
  }

  getUserBooks() {
    this.swagger.api_Book_GetAllForUser().subscribe((res) => {
      this.books = res;
    });
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
    return;
  }
}
