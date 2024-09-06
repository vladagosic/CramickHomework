import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ApiException, ProblemDetails } from '../clients/api-client';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {
  constructor(
    private toastr: ToastrService
  ) { }

  public Handle(error: any): void {
    if (error instanceof ApiException) {
      this.HandleApiExceptionError(error);
      return;
    }
    if (error instanceof ProblemDetails) {
      this.HandleProblemDetailsError(error);
      return;
    }
    this.HandleGenericError(error);
  }

  private HandleApiExceptionError(error: ApiException): void {
    const response = (error.response) ? JSON.parse(error.response) : {};
    let message = "An unexpected error occurred!";
    let title = "";

    if (response.hasOwnProperty("detail")) {
      message = response["detail"];
    }
    if (response.hasOwnProperty("title")) {
      title = response["title"];
    }
    
    this.toastr.error(message, title);
  }
  private HandleProblemDetailsError(error: ProblemDetails): void {
    let title = error.title!;
    let message = "";

    if (error.hasOwnProperty("errors")) {
      let errors = error["errors"];
      if (!this.isEmptyObject(errors)) {
        message = "<ul>"
        for (var property in errors) {
          message += `<li>${errors[property]}</li>`;
        }
        message = message.substring(0, message.length - 3) + "</ul>";
        message = message;
      } else {
        message = error.detail!;
      }
    } else {
      message = error.detail!;
    }

    this.toastr.warning(message, title, { enableHtml: true });
  }

  private HandleGenericError(error: any): void {
    if (!this.isEmptyObject(error)) {
      let message = "<ul>";
      for (var property in error) {
        message += `<li>${property}: ${error[property]}, </li>`;
      }
      message = message.substring(0, message.length - 3) + "</ul>";
      this.toastr.error(message, "An unexpected error occurred!", { enableHtml: true });
    } else {
      this.toastr.error("An unexpected error occurred!");
    }
  }

  private isEmptyObject(obj: any): boolean {
    for (var prop in obj) {
      if (Object.prototype.hasOwnProperty.call(obj, prop)) {
        return false;
      }
    }
    return true;
  }

}
