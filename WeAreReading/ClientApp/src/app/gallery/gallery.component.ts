import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { BookDTO, AuthTicketDTO, SwaggerClient } from '../services/SwaggerClient.service';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
import { environment } from 'src/environments/environment';
import { fromEvent } from 'rxjs';
import { map, filter, debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.css']
})
export class GalleryComponent implements OnInit {
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
    private toastr: ToastrService,
    private authService: AuthService
  ) { }

  ngAfterViewInit(): void {
    this.subscribeSearchChange();
  }

  ngOnInit(): void {
    this.env = environment;
    this.currentUser = this.userService.CurrentUser;
    this.getBooks(undefined);
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
      this.getBooks(text);
    });
  }

  getBooks(search: string = undefined) {
    const authenticated = this.authService.isAuthenticated();
    if (authenticated) {
      this.swagger.api_Book_GetAllForOthers(search).subscribe(res => {
        this.books = res;
      })
    } else {
      this.swagger.api_Book_GetAll(search).subscribe((res) => {
        this.books = res;
        this.isSearching = false;
      });
    }
  }

  getRecommendedBooks() {
    this.swagger.api_Book_GetRecommendedBooks(4).subscribe(res => {
      this.recommendedBooks = res;
    })
  }

  onDetailsClick($event, book: BookDTO) {
    $event.preventDefault();
    this.router.navigate(['/books', book.id]);
  }

}
