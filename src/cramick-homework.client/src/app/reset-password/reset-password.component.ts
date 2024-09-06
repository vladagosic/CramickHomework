import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../shared/services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorHandlerService } from '../shared/services/error-handler.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {
  public sendResetForm: FormGroup = new FormGroup({
    email: new FormControl('')
  });
  public resetForm: FormGroup = new FormGroup({
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });
  public isReset = false;
  public submitted = false;
  public isSuccess = false;
  public isSent = false;
  private email: string | null = "";
  private token: string | null = "";

  constructor(
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder,
    private errorHanlder: ErrorHandlerService ) { }

  ngOnInit(): void {
    this.email = this.route.snapshot.queryParamMap.get('email');
    this.token = this.route.snapshot.queryParamMap.get('token');

    if (this.email && this.token) {
      this.isReset = true;

    } else {
      this.isReset = false;
      this.sendResetForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email, Validators.maxLength(256)]]
      });
    }
  }

  public get f(): { [key: string]: AbstractControl } {
    return (this.isReset) ? this.resetForm.controls : this.sendResetForm.controls;
  }

  public onSendSubmit(): void {
    this.submitted = true;

    if (this.sendResetForm.invalid) {
      return;
    }

    this.isSent = true;

    this.authenticationService.sendPasswordResetEmail(this.sendResetForm.get('email')!.value)
      .subscribe({
        next: (success) => {
          this.isSuccess = success;
        },
        error: (error) => {
          this.isSuccess = false;
          this.errorHanlder.Handle(error);
        }
      });
  }

  public onResetSubmit(): void {
    this.submitted = true;

    if (this.resetForm.invalid) {
      return;
    }

    this.isSent = true;

    const password = this.resetForm.get('password')!.value;

    this.authenticationService.resetPassword(this.email!, password, this.token!)
      .subscribe({
        next: (success) => {          
          this.isSuccess = success;
        },
        error: (error) => {
          this.isSuccess = false;
          this.errorHanlder.Handle(error);
        }
      });
  }
}
