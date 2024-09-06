import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UsersService } from '../shared/services/users.service';
import Validation from '../helpers/validation';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ErrorHandlerService } from '../shared/services/error-handler.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent implements OnInit {
  public createUserForm: FormGroup = new FormGroup({
    fullname: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
  });
  public submitted = false;

  constructor(
    private userService: UsersService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private errorHandler: ErrorHandlerService) { }


  ngOnInit(): void {
    this.createUserForm = this.formBuilder.group(
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
    return this.createUserForm.controls;
  }

  onReset(): void {
    this.submitted = false;
    this.createUserForm.reset();
  }

  public onSubmit() {
    this.submitted = true;

    if (this.createUserForm.invalid) {
      return;
    }

    this.userService.createUser(
      this.createUserForm.get('email')!.value,
      this.createUserForm!.get('password')!.value,
      this.createUserForm.get('fullname')!.value
    ).subscribe({
      next: (result) => {
        this.toastr.success(`User ${ result.fullName } successfully created.`);
        this.router.navigate(['/users']);
      },
      error: (error) => {
        this.errorHandler.Handle(error);
      }
    });
  }

}
