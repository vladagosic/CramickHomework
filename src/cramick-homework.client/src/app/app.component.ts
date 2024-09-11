import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './shared/services/authentication.service';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  constructor(
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    
  }

  public get isLoggedIn(): boolean {
    return this.authenticationService.isLoggedIn();
  }
 
}
