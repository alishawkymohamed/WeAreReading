import {
  Component,
  OnInit,
  Input,
  ViewEncapsulation,
  ViewChild,
} from "@angular/core";
import {
  RequestDTO,
  SwaggerClient,
  UserDTO,
} from "src/app/services/SwaggerClient.service";
import { SwalComponent } from "@sweetalert2/ngx-sweetalert2";

@Component({
  selector: "app-accepted-sent-requests",
  templateUrl: "./accepted-sent-requests.component.html",
  styleUrls: ["./accepted-sent-requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class AcceptedSentRequestsComponent {
  @Input() acceptedSentRequests: RequestDTO[];
  @Input() displayed: boolean;
  contactText: string;
  @ViewChild("contactSwal", { static: true })
  private contactSwal: SwalComponent;

  constructor(private swagger: SwaggerClient) {}

  onViewContacts(request: RequestDTO) {
    this.swagger
      .api_Account_GetUserDetails(request.receiverId)
      .subscribe((res) => {
        this.contactText = `
          Name: ${res.fullName}<br>
          Username: ${res.username}<br>
          Phone: ${res.phoneNumber}<br>
          Email: ${res.email}<br>
          Location: Egypt, ${res.governmentName}
          `;
        setTimeout(() => {
          this.contactSwal.fire();
        }, 1000);
      });
  }
}
