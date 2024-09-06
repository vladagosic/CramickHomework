import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../shared/services/authentication.service';
import Validation from '../helpers/validation';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  public registerForm: FormGroup = new FormGroup({
    fullname: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });
  public submitted = false;
  
  constructor(
    private authenticationService: AuthenticationService,
    private formBuilder: FormBuilder) {}

  ngOnInit() {
    this.authenticationService.removeToken();
    
    this.registerForm = this.formBuilder.group(
      {
        fullname: ['', Validators.required],
        email: ['', [Validators.required, Validators.email, Validators.maxLength(256)]],
        password: [ '', Validators.required],
        confirmPassword: ['', Validators.required]
      },
      {
        validators: [Validation.match('password', 'confirmPassword')]
      }
    );
  }

  public get f(): { [key: string]: AbstractControl } {
    return this.registerForm.controls;
  }

  onReset(): void {
    this.submitted = false;
    this.registerForm.reset();
  }

  public onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    this.authenticationService.register(
      this.registerForm.get('email')!.value,
      this.registerForm!.get('password')!.value,
      this.registerForm.get('fullname')!.value
    );
  }

}
