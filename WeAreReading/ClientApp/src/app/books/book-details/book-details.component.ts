import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
  bookId: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getBookId();
  }

  getBookId() {
    this.route.params.subscribe(params => {
      this.bookId = params['id'];
      if (!this.bookId || !Number.isInteger(parseInt(this.bookId))) {
        this.router.navigate(['/not-found']);
      }
    });
  }

}
