import { Component, Input, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  selector: 'app-viewimagecell',
  templateUrl: './viewimagecell.component.html',
  styleUrls: ['./viewimagecell.component.scss'],
})
export class ViewImageCellComponent implements ViewCell, OnInit {
  @Input() value: string;
  @Input() rowData: any;
  constructor() {}
  ngOnInit() {}

  get image() {
    if (!this.value) {
      return null;
    }
    var fileExt = this.value.split(',')[0].split('.').pop();
    if (
      fileExt == 'png' ||
      fileExt == 'jpg' ||
      fileExt == 'jpeg' ||
      fileExt == 'icon'
    ) {
      return this.value.split(',')[0];
    }
    return '';
  }
}
