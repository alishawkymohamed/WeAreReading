import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { RequestDTO, SwaggerClient } from "../services/SwaggerClient.service";
import { ToastrService } from "ngx-toastr";
import { UserService } from "../services/user.service";

@Component({
  selector: "app-requests",
  templateUrl: "./requests.component.html",
  styleUrls: ["./requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class RequestsComponent implements OnInit {
  isNotRespondedReceivedDisplayed = true;
  isNotRespondedSentDisplayed = false;
  isAcceptedSentDisplayed = false;
  isRejectedSentDisplayed = false;
  isAcceptedReceivedDisplayed = false;
  isRejectedReceivedDisplayed = false;
  notRespondedReceivedRequests: RequestDTO[];
  notRespondedSentRequests: RequestDTO[];
  acceptedSentRequests: RequestDTO[];
  rejectedSentRequests: RequestDTO[];
  acceptedReceivedRequests: RequestDTO[];
  rejectedReceivedRequests: RequestDTO[];

  constructor(private swagger: SwaggerClient) {}

  ngOnInit(): void {
    this.getNotRespondedReceivedRequests();
    this.getNotRespondedSentRequests();
    this.getAcceptedReceivedRequests();
    this.getAcceptedSentRequests();
    this.getRejectedReceivedRequests();
    this.getRejectedSentRequests();
  }

  OnNotRespondedReceivedClicked(el: HTMLElement) {
    this.toggleDisplay(el);
    this.isNotRespondedReceivedDisplayed = true;
    this.isNotRespondedSentDisplayed = false;
    this.isAcceptedReceivedDisplayed = false;
    this.isAcceptedSentDisplayed = false;
    this.isRejectedReceivedDisplayed = false;
    this.isRejectedSentDisplayed = false;
  }

  OnNotRespondedSentClicked(el: HTMLElement) {
    this.toggleDisplay(el);
    this.isNotRespondedReceivedDisplayed = false;
    this.isNotRespondedSentDisplayed = true;
    this.isAcceptedReceivedDisplayed = false;
    this.isAcceptedSentDisplayed = false;
    this.isRejectedReceivedDisplayed = false;
    this.isRejectedSentDisplayed = false;
  }

  OnAcceptedReceivedClicked(el: HTMLElement) {
    this.toggleDisplay(el);
    this.isNotRespondedReceivedDisplayed = false;
    this.isNotRespondedSentDisplayed = false;
    this.isAcceptedReceivedDisplayed = true;
    this.isAcceptedSentDisplayed = false;
    this.isRejectedReceivedDisplayed = false;
    this.isRejectedSentDisplayed = false;
  }

  OnRejectedReceivedClicked(el: HTMLElement) {
    this.toggleDisplay(el);
    this.isNotRespondedReceivedDisplayed = false;
    this.isNotRespondedSentDisplayed = false;
    this.isAcceptedReceivedDisplayed = false;
    this.isAcceptedSentDisplayed = false;
    this.isRejectedReceivedDisplayed = true;
    this.isRejectedSentDisplayed = false;
  }

  OnAcceptedSentClicked(el: HTMLElement) {
    this.toggleDisplay(el);
    this.isNotRespondedReceivedDisplayed = false;
    this.isNotRespondedSentDisplayed = false;
    this.isAcceptedReceivedDisplayed = false;
    this.isAcceptedSentDisplayed = true;
    this.isRejectedReceivedDisplayed = false;
    this.isRejectedSentDisplayed = false;
  }

  OnRjecetedSentClicked(el: HTMLElement) {
    this.toggleDisplay(el);
    this.isNotRespondedReceivedDisplayed = false;
    this.isNotRespondedSentDisplayed = false;
    this.isAcceptedReceivedDisplayed = false;
    this.isAcceptedSentDisplayed = false;
    this.isRejectedReceivedDisplayed = false;
    this.isRejectedSentDisplayed = true;
  }

  toggleDisplay(el: HTMLElement) {
    const els = document.querySelectorAll(".nav-link");
    for (let i = 0; i < els.length; i++) {
      els[i].classList.remove("active");
    }
    el.classList.add("active");
  }

  getNotRespondedReceivedRequests() {
    this.swagger
      .api_Request_GetNotRespondedReceivedRequests()
      .subscribe((res) => {
        this.notRespondedReceivedRequests = res;
      });
  }

  getNotRespondedSentRequests() {
    this.swagger.api_Request_GetNotRespondedSentRequests().subscribe((res) => {
      this.notRespondedSentRequests = res;
    });
  }

  getAcceptedSentRequests() {
    this.swagger.api_Request_GetAcceptedSentRequests().subscribe((res) => {
      this.acceptedSentRequests = res;
    });
  }

  getAcceptedReceivedRequests() {
    this.swagger.api_Request_GetAcceptedReceivedRequests().subscribe((res) => {
      this.acceptedReceivedRequests = res;
    });
  }

  getRejectedSentRequests() {
    this.swagger.api_Request_GetRejectedSentRequests().subscribe((res) => {
      this.rejectedSentRequests = res;
    });
  }

  getRejectedReceivedRequests() {
    this.swagger.api_Request_GetRejectedReceivedRequests().subscribe((res) => {
      this.rejectedReceivedRequests = res;
    });
  }

  onNotRespondedReceivedChanged($event) {
    this.getNotRespondedReceivedRequests();
    this.getAcceptedReceivedRequests();
    this.getRejectedReceivedRequests();
  }

  onNotRespondedSentChanged($event) {
    this.getNotRespondedSentRequests();
  }
}
