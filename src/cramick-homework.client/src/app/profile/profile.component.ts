import { Component, OnInit } from '@angular/core';
import { GetUsersUserModel } from '../shared/clients/api-client';
import { UsersService } from '../shared/services/users.service';
import { ErrorHandlerService } from '../shared/services/error-handler.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  public user: GetUsersUserModel = new GetUsersUserModel();

  constructor(
    private usersService: UsersService,
    private errorHandler: ErrorHandlerService
  ) { }

  ngOnInit(): void {
    this.usersService.getCurrentUser()
      .subscribe({
        next: (user) => this.user = user,
        error: (error) => this.errorHandler.Handle(error)
      });
  }
}
