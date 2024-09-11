import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DefaultComponent } from './default/default.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ApiClient } from './shared/clients/api-client';
import { TokenInterceptor } from './helpers/token.interceptor';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { ProfileComponent } from './profile/profile.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { provideToastr } from 'ngx-toastr';
import { UsersComponent } from './users/users.component';
import { ContactsComponent } from './contacts/contacts.component';

@NgModule({
  declarations: [
    AppComponent,
    DefaultComponent,
    LoginComponent,
    RegisterComponent,
    ResetPasswordComponent,
    ConfirmEmailComponent,
    NavbarComponent,
    ProfileComponent,
    CreateUserComponent,    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    UsersComponent,    
    ContactsComponent,
  ],
  providers: [
    ApiClient,
    provideHttpClient(
      withInterceptors([TokenInterceptor])
    ),
    provideAnimationsAsync(),
    provideToastr()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
