import { Injectable } from "@angular/core";
import { ApiClient, CreateUserRequest, GetCurrentUserUserModel, GetUsersUserModel, PagedResponseOfGetUsersUserModel } from "../clients/api-client";
import { Router } from "@angular/router";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class UsersService {

    constructor(
        private apiClient: ApiClient,
        private router: Router) { }

    public getUsers(pageSize: number, pageNumber: number, sort: string | undefined): Observable<PagedResponseOfGetUsersUserModel> {
        return this.apiClient.users_GetAll(pageSize, pageNumber, sort);
    }

    public getCurrentUser(): Observable<GetCurrentUserUserModel> {
        return this.apiClient.users_GetCurrentUser();
    }

    public createUser(email: string, password: string, fullName: string): Observable<GetUsersUserModel> {
       return this.apiClient.users_CreateUser(new CreateUserRequest({ email: email, password: password, fullName: fullName }));
    }
}