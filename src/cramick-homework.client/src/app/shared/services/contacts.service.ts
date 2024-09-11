import { Injectable } from "@angular/core";
import { ApiClient, CreateOrUpdateContactRequest, DeleteContactRequest, GetContactsContactModel, PagedResponseOfGetContactsContactModel } from "../clients/api-client";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class ContactsService {

    constructor(private apiClient: ApiClient) { }

    public getContacts(pageSize: number, pageNumber: number, sort: string | undefined): Observable<PagedResponseOfGetContactsContactModel> {
        return this.apiClient.contacts_GetAll(pageSize, pageNumber, sort);
    }

    public createOrUpdateContact(id?: string, name?: string, phone?: string): Observable<GetContactsContactModel> {
       return this.apiClient.contacts_CreateOrUpdate(new CreateOrUpdateContactRequest({ id: id, name: name, phone: phone }));
    }

    public deleteContact(id?: string): Observable<void> {
        return this.apiClient.contacts_Delete(new DeleteContactRequest({ id: id }))
    }
}