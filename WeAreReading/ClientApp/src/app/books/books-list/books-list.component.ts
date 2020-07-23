import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from "@angular/core";
import { SwaggerClient, BookDTO, AuthTicketDTO } from "src/app/services/SwaggerClient.service";
import { environment } from "src/environments/environment";
import { UserService } from "src/app/services/user.service";
import { ToastrService } from "ngx-toastr";
import { Router } from '@angular/router';
import { of, fromEvent } from "rxjs";
import {
  debounceTime,
  map,
  distinctUntilChanged,
  filter
} from "rxjs/operators";

@Component({
  selector: "app-books-list",
  templateUrl: "./books-list.component.html",
  styleUrls: ["./books-list.component.css"],
})
export class BooksListComponent implements OnInit, AfterViewInit {
  books: BookDTO[];
  recommendedBooks: BookDTO[];
  env: any;
  currentUser: AuthTicketDTO;
  @ViewChild('search') searchInput: ElementRef;
  isSearching = false;

  constructor(
    private swagger: SwaggerClient,
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngAfterViewInit(): void {
    this.subscribeSearchChange();
  }

  ngOnInit(): void {
    this.env = environment;
    this.currentUser = this.userService.CurrentUser;
    this.getUserBooks();
    this.getRecommendedBooks();
  }

  subscribeSearchChange() {
    fromEvent(this.searchInput.nativeElement, 'input').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , filter(res => res.length >= 0)
      , debounceTime(1000)
      , distinctUntilChanged()
    ).subscribe((text: string) => {
      this.isSearching = true;
      this.getUserBooks(text);
    });
  }

  getUserBooks(search: string = undefined) {
    this.swagger.api_Book_GetAllForUser(undefined, undefined, search).subscribe((res) => {
      this.books = res;
      this.isSearching = false;
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
