import { Component } from '@angular/core';
import { AuthenticationService } from '../shared/services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
 
  constructor(
    private authenticationService: AuthenticationService) { }

  public logout(): void {
    this.authenticationService.logout();
  }
}
