import { Component, OnInit, ViewEncapsulation, Input } from "@angular/core";
import { RequestDTO } from "src/app/services/SwaggerClient.service";

@Component({
  selector: "app-rejected-sent-requests",
  templateUrl: "./rejected-sent-requests.component.html",
  styleUrls: ["./rejected-sent-requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class RejectedSentRequestsComponent implements OnInit {
  @Input() rejectedSentRequests: RequestDTO[];
  @Input() displayed: boolean;

  constructor() {}

  ngOnInit(): void {}
}
