import {
  Component,
  OnInit,
  Input,
  ViewEncapsulation,
  Output,
  EventEmitter,
  ViewChild,
} from "@angular/core";
import {
  RequestDTO,
  SwaggerClient,
} from "src/app/services/SwaggerClient.service";
import { ToastrService } from "ngx-toastr";
import { SwalComponent } from "@sweetalert2/ngx-sweetalert2";

@Component({
  selector: "app-not-responded-received-requests",
  templateUrl: "./not-responded-received-requests.component.html",
  styleUrls: ["./not-responded-received-requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class NotRespondedReceivedRequestsComponent {
  @Input() notRespondedReceivedRequests: RequestDTO[];
  @Input() displayed: boolean;
  @Output() changed = new EventEmitter<boolean>();
  requestToReject: RequestDTO;
  requestToAccept: RequestDTO;
  @ViewChild("rejectSwal", { static: true }) private rejectSwal: SwalComponent;
  @ViewChild("acceptSwal", { static: true }) private acceptSwal: SwalComponent;

  constructor(private swagger: SwaggerClient, private toastr: ToastrService) {}

  onAccept(request: RequestDTO) {
    this.requestToAccept = request;
    this.acceptSwal.fire();
  }

  onReject(request: RequestDTO) {
    this.requestToReject = request;
    this.rejectSwal.fire();
  }

  rejectRequest(request: RequestDTO) {
    this.swagger.api_Request_RejectRequest(request.id).subscribe((res) => {
      this.toastr.warning("Rejected Successfully");
      this.changed.emit(true);
    });
  }

  acceptRequest(request: RequestDTO) {
    this.swagger.api_Request_AcceptRequest(request.id).subscribe((res) => {
      this.toastr.success("Accepted Successfully");
      this.changed.emit(true);
    });
  }
}
