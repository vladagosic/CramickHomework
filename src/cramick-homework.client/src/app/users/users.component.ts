import { Component, Directive, EventEmitter, Input, OnInit, Output, QueryList, ViewChildren } from '@angular/core';
import { GetUsersUserModel } from '../shared/clients/api-client';
import { DatePipe, DecimalPipe } from '@angular/common';
import { UsersService } from '../shared/services/users.service';
import { NgbPaginationModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

export type SortColumn = keyof GetUsersUserModel | '';
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
  selector: 'app-users',
  standalone: true,
	imports: [DatePipe, FormsModule, NgbTypeaheadModule, NgbPaginationModule, NgbdSortableHeader, RouterLink],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  public users: GetUsersUserModel[] = [];
  page = 1;
  pageSize = 10;
  sort = "";
  total = 0;

  @ViewChildren(NgbdSortableHeader)
  headers!: QueryList<NgbdSortableHeader>;

  constructor(
    private usersService: UsersService
  ) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.usersService.getUsers(this.pageSize, this.page, this.sort)
      .subscribe((response) => {
        this.total = response.total!;
        this.users = (response.items) ? response.items : [];
      });
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

    this.loadUsers();
  }
}
