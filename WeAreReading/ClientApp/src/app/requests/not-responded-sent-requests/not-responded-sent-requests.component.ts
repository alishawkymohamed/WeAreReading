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
  SwaggerClient,
  RequestDTO,
} from "src/app/services/SwaggerClient.service";
import { ToastrService } from "ngx-toastr";
import { SwalComponent } from "@sweetalert2/ngx-sweetalert2";

@Component({
  selector: "app-not-responded-sent-requests",
  templateUrl: "./not-responded-sent-requests.component.html",
  styleUrls: ["./not-responded-sent-requests.component.css"],
  encapsulation: ViewEncapsulation.None,
})
export class NotRespondedSentRequestsComponent {
  @Input() displayed: boolean;
  @Input() notRespondedSentRequests: RequestDTO[];
  @Output() changed = new EventEmitter<boolean>();
  requestToDelete: RequestDTO;
  @ViewChild("deleteSwal", { static: true }) private deleteSwal: SwalComponent;

  constructor(private swagger: SwaggerClient, private toastr: ToastrService) {}

  onDelete(request: RequestDTO) {
    this.requestToDelete = request;
    this.deleteSwal.fire();
  }

  deleteRequest(request: RequestDTO) {
    this.swagger.api_Request_DeleteRequest(request.id).subscribe((res) => {
      this.toastr.success("Deleted Successfully");
      this.changed.emit(true);
    });
  }
}
