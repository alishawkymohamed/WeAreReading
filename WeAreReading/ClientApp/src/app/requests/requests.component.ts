import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { RequestDTO, SwaggerClient } from "../services/SwaggerClient.service";
import { ToastrService } from "ngx-toastr";
import { UserService } from "../services/user.service";

@Component({
  selector: "app-requests",
  templateUrl: "./requests.component.html",
  styleUrls: ["./requests.component.css"],
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

  constructor(
    private swagger: SwaggerClient,
    private toastr: ToastrService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.getReceivedRequests();
    this.getSentRequests();
  }

  OnTabClicked(el: HTMLElement) {
    this.toggleDisplay(el);
  }

  toggleDisplay(el: HTMLElement) {
    const els = document.querySelectorAll(".nav-link");
    for (let i = 0; i < els.length; i++) {
      els[i].classList.remove("active");
    }
    el.classList.add("active");
    this.isNotRespondedReceivedDisplayed = !this
      .isNotRespondedReceivedDisplayed;
    this.isNotRespondedSentDisplayed = !this.isNotRespondedSentDisplayed;
    this.isAcceptedReceivedDisplayed = !this.isAcceptedReceivedDisplayed;
    this.isAcceptedSentDisplayed = !this.isAcceptedSentDisplayed;
    this.isRejectedReceivedDisplayed = !this.isRejectedReceivedDisplayed;
    this.isRejectedSentDisplayed = !this.isRejectedSentDisplayed;
  }

  getReceivedRequests() {
    this.swagger
      .api_Request_GetReceivedNotRespondedRequests()
      .subscribe((res) => {
        this.notRespondedReceivedRequests = res;
      });
  }

  getSentRequests() {
    this.swagger.api_Request_GetSentNotRespondedRequests().subscribe((res) => {
      this.notRespondedSentRequests = res;
    });
  }

  onAccept(request: RequestDTO) {
    console.log(request);
  }

  onReject(request: RequestDTO) {
    console.log(request);
  }

  onDelete(request: RequestDTO) {
    console.log(request);
  }
}
