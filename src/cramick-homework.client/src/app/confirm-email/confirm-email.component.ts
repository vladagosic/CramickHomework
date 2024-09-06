import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../shared/services/authentication.service';
import { ErrorHandlerService } from '../shared/services/error-handler.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.css'
})
export class ConfirmEmailComponent implements OnInit {
  public isConfirmation = false;
  public isSuccess = false;

  private email: string | null = "";
  private token: string | null = "";

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private errorHandler: ErrorHandlerService) { }

  ngOnInit(): void {
    this.email = this.route.snapshot.queryParamMap.get('email');
    this.token = this.route.snapshot.queryParamMap.get('token');

    if (this.email && this.token) {
      this.isConfirmation = true;
      this.authenticationService.confirmEmail(this.email, this.token)
        .subscribe({
          next: (confirmed) => {
            this.isSuccess = confirmed;
            if (confirmed) {
              if (this.authenticationService.isLoggedIn()) {
                setTimeout(() => this.router.navigate(['/']), 5000);}
            }
          },
          error: (error) => {
            this.isSuccess = false;
            this.errorHandler.Handle(error);
          }
        });
    } else {
      this.isConfirmation = false;
      this.route.params.subscribe((params) => {
        this.email = params['email'];
      });
    }
  }

  public get isLoggedIn(): boolean {
    return this.authenticationService.isLoggedIn();
  }

  public resendConfirmationEmail(): void {
    this.authenticationService.resendConfirmationEmail(this.email!);
  }

}
