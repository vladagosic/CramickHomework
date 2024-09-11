import { Component, Directive, EventEmitter, inject, Input, Output, QueryList, TemplateRef, ViewChildren } from '@angular/core';
import { GetContactsContactModel } from '../shared/clients/api-client';
import { ContactsService } from '../shared/services/contacts.service';
import { NgbModal, NgbPaginationModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule, DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { ErrorHandlerService } from '../shared/services/error-handler.service';

export type SortColumn = keyof GetContactsContactModel | '';
export type SortDirection = 'asc' | 'desc' | '';
const rotate: { [key: string]: SortDirection } = { asc: 'desc', desc: '', '': 'asc' };

export interface SortEvent {
  column: SortColumn;
  direction: SortDirection;
}

@Directive({
  selector: 'th[sortable]',
  standalone: true,
  host: {
    '[class.asc]': 'direction === "asc"',
    '[class.desc]': 'direction === "desc"',
    '(click)': 'rotate()',
  },
})
export class NgbdSortableHeader {
  @Input() sortable: SortColumn = '';
  @Input() direction: SortDirection = '';
  @Output() sort = new EventEmitter<SortEvent>();

  rotate() {
    this.direction = rotate[this.direction];
    this.sort.emit({ column: this.sortable, direction: this.direction });
  }
}

@Component({
  selector: 'app-contacts',
  standalone: true,
  imports: [CommonModule, DatePipe, FormsModule, ReactiveFormsModule, NgbTypeaheadModule, NgbPaginationModule, NgbdSortableHeader],
  templateUrl: './contacts.component.html',
  styleUrl: './contacts.component.css'
})
export class ContactsComponent {
  private modalService = inject(NgbModal);
  public contacts: GetContactsContactModel[] = [];
  public contact: GetContactsContactModel = new GetContactsContactModel();
  page = 1;
  pageSize = 10;
  sort = "";
  total = 0;
  submitted = false;

  @ViewChildren(NgbdSortableHeader)
  headers!: QueryList<NgbdSortableHeader>;

  public contactForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    phone: new FormControl('')
  });

  constructor(
    private contactsService: ContactsService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private errorHandler: ErrorHandlerService) { }

  ngOnInit(): void {
    this.contactForm = this.formBuilder.group(
      {
        name: ['', [Validators.required, Validators.maxLength(256)]],
        phone: ['', Validators.maxLength(20)]
      }
    );
    this.loadContacts();
  }

  public get f(): { [key: string]: AbstractControl } {
    return this.contactForm.controls;
  }

  loadContacts() {
    this.contactsService.getContacts(this.pageSize, this.page, this.sort)
      .subscribe((response) => {
        this.total = response.total!;
        this.contacts = (response.items) ? response.items : [];
      });
  }

  add(content: TemplateRef<any>): void {
    this.contact = new GetContactsContactModel();
    this.setValues();
    this.submitted = false;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  edit(content: TemplateRef<any>, contact: GetContactsContactModel): void {
    this.contact = contact;
    this.setValues();
    this.submitted = false;
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  delete(contact: GetContactsContactModel): void {
    this.contactsService.deleteContact(contact.id)
      .subscribe({
        next: () => {
          this.toastr.success(`User ${contact.name} successfully deleted.`);
          if (this.page > 1 && this.contacts.length == 1) {
            this.page--;
          }
          this.loadContacts();
        },
        error: (error) => {
          this.errorHandler.Handle(error);
        }
      });
  }

  save(): void {
    this.submitted = true;

    if (this.contactForm.invalid) {
      return;
    }

    this.contactsService.createOrUpdateContact(this.contact.id, this.contactForm.get("name")?.value, this.contactForm.get("phone")?.value)
    .subscribe({
      next: (result) => {
        this.toastr.success(`User ${result.name} successfully ${ (this.contact.id) ? "updated" : "created" }.`);
        this.modalService.dismissAll();
        this.loadContacts();
      },
      error: (error) => {
        this.errorHandler.Handle(error);
      }
    });
  }

  cancel(): void {
    this.modalService.dismissAll();
  }

  private setValues(): void {
    this.contactForm.get("name")?.setValue(this.contact.name);
    this.contactForm.get("phone")?.setValue(this.contact.phone);
  }

  onSort({ column, direction }: SortEvent) {
    // resetting other headers
    for (const header of this.headers) {
      if (header.sortable !== column) {
        header.direction = '';
      }
    }

    if (direction === '' || column === '') {
      this.sort = "";
    } else {
      this.sort = (direction == "asc") ? column : "!" + column;
    }

    this.loadContacts();
  }
}
