import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });
  public submitted = false;

  constructor(
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.authenticationService.logout();

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email, Validators.maxLength(256)]],
      password: ['', Validators.required],
    });
  }

  public get f(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
  }

  public onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.authenticationService.login(
      this.loginForm.get('email')!.value,
      this.loginForm.get('password')!.value
    );
  }
}
