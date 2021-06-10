import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { range } from 'lodash';

@Component({
  selector: 'app-paging-server-view',
  templateUrl: './paging-server-view.component.html',
  styleUrls: ['./paging-server-view.component.scss']
})
export class PagingServerViewComponent implements OnInit {
  @Input() data: any;
  @Output() page = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  get pagelist() {
    if (!this.data) {
      return [];
    }
    if (this.data.totalPage < 5) {
      return range(1, this.data.totalPage + 1);
    }
    if (this.data.pageIndex <= 2) {
      return range(1, 5);
    }
    if (this.data.pageIndex > this.data.totalPage - 2) {
      return range(this.data.totalPage - 3, this.data.totalPage + 1);
    }
    return this.data
      ? range(this.data.pageIndex - 1, this.data.pageIndex + 3)
      : [];
  }

  onPageIndexChange(pageIndex: number, pageSize: number = 10)
  {
    this.page.emit(pageIndex);
  }
}
