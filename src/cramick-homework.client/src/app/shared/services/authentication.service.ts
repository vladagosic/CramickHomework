import { Injectable } from "@angular/core";
import { ApiClient, LoginRequest, RegisterRequest } from "../clients/api-client";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { ResetPasswordRequest } from "../clients/api-client";
import { ErrorHandlerService } from "./error-handler.service";

@Injectable({
    providedIn: 'root',
})
export class AuthenticationService {
    private tokenKey = 'token';

    constructor(
        private apiClient: ApiClient,
        private router: Router,
        private errorHandler: ErrorHandlerService) { }

    public login(email: string, password: string): void {
        this.apiClient.authentication_Login(new LoginRequest({ email: email, password: password }))
            .subscribe({
                next: (token) => {
                    localStorage.setItem(this.tokenKey, token);
                    this.router.navigate(['/']);
                },
                error: (error) => {
                    this.errorHandler.Handle(error);
                }
            });
    }

    public register(email: string, password: string, fullName: string): void {
        this.apiClient.authentication_Register(new RegisterRequest({ email: email, password: password, fullName: fullName }))
            .subscribe({
                next: (token) => {
                    if (token) {
                        localStorage.setItem(this.tokenKey, token);
                        this.router.navigate(['/']);
                    }
                    else {
                        this.router.navigate(['/confirm-email', { email: email }]);
                    }
                },
                error: (error) => {
                    this.errorHandler.Handle(error);
                }
            });
    }

    public confirmEmail(email: string, token: string): Observable<boolean> {
        return this.apiClient.authentication_ConfirmEmail(email, token);
    }

    public resendConfirmationEmail(email: string): Observable<boolean> {
        return this.apiClient.authentication_ResendConfirmation(email);
    }

    public sendPasswordResetEmail(email: string): Observable<boolean> {
        return this.apiClient.authentication_SendPasswordReset(email);
    }

    public resetPassword(email: string, password: string, token: string): Observable<boolean> {
        return this.apiClient.authentication_ResetPassword(new ResetPasswordRequest({ email: email, password: password, token: token }));
    }

    public logout(): void {
        this.removeToken();
        this.router.navigate(['/login']);
    }

    public removeToken(): void {
        localStorage.removeItem(this.tokenKey);
    }

    public isLoggedIn(): boolean {
        let token = localStorage.getItem(this.tokenKey);
        return token != null && token.length > 0;
    }

    public getToken(): string | null {
        return this.isLoggedIn() ? localStorage.getItem(this.tokenKey) : null;
    }
}