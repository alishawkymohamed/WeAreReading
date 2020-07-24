import {
  Component,
  OnInit,
  ViewEncapsulation,
  Input,
  ViewChild,
} from "@angular/core";
import {
  RequestDTO,
  SwaggerClient,
  UserDTO,
} from "src/app/services/SwaggerClient.service";
import { SwalComponent } from "@sweetalert2/ngx-sweetalert2";

@Component({
  selector: "app-accepted-received-requests",
  templateUrl: "./accepted-received-requests.component.html",
  styleUrls: ["./accepted-received-requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class AcceptedReceivedRequestsComponent {
  @Input() acceptedReceivedRequests: RequestDTO[];
  @Input() displayed: boolean;
  contactText: string;
  @ViewChild("contactSwal", { static: true })
  private contactSwal: SwalComponent;

  constructor(private swagger: SwaggerClient) {}

  onViewContacts(request: RequestDTO) {
    this.swagger
      .api_Account_GetUserDetails(request.senderId)
      .subscribe((res) => {
        this.contactText = `
          Name: ${res.fullName} <br>
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
