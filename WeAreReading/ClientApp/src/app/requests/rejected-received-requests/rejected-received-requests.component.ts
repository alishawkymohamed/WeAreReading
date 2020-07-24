import { Component, OnInit, ViewEncapsulation, Input } from "@angular/core";
import { RequestDTO } from "src/app/services/SwaggerClient.service";

@Component({
  selector: "app-rejected-received-requests",
  templateUrl: "./rejected-received-requests.component.html",
  styleUrls: ["./rejected-received-requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class RejectedReceivedRequestsComponent implements OnInit {
  @Input() rejectedReceivedRequests: RequestDTO[];
  @Input() displayed: boolean;

  constructor() {}

  ngOnInit(): void {}

  onViewContacts(request: RequestDTO) {
    console.log(request);
  }
}
