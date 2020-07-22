import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SwaggerClient, BookDTO, UserDTO } from 'src/app/services/SwaggerClient.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
  bookId: string;
  book: BookDTO;
  env: any;
  ownerDto: UserDTO;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private swagger: SwaggerClient
  ) { }

  ngOnInit(): void {
    this.env = environment;
    this.getBookId();
    this.getBookDetails();
  }

  getBookId() {
    this.route.params.subscribe(params => {
      this.bookId = params['id'];
      if (!this.bookId || !Number.isInteger(parseInt(this.bookId))) {
        this.router.navigate(['/not-found']);
      }
    });
  }

  getBookDetails() {
    this.swagger.api_Book_GetDetails(+this.bookId).subscribe(res => {
      this.book = res;
      this.getOwnerDetails(this.book.ownerId);
    });
  }

  getOwnerDetails(userId: number) {
    this.swagger.api_Account_GetUserDetails(userId).subscribe(res => {
      this.ownerDto = res;
      console.log(this.ownerDto);
    })
  }
}
