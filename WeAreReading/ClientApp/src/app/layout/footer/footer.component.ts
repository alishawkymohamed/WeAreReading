import { Component, OnInit } from '@angular/core';
import { SwaggerClient, BookDTO } from 'src/app/services/SwaggerClient.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  lastAddedBooks: BookDTO[];
  env: any;

  constructor(private swagger: SwaggerClient) { }

  ngOnInit(): void {
    this.env = environment;
    this.getLastAddedBooks();
  }

  getLastAddedBooks() {
    this.swagger.api_Book_GetLastAddedBooks(3).subscribe(res => {
      this.lastAddedBooks = res;
    })
  }

}
