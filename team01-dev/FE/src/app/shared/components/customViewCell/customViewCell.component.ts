import { Component, Input, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  selector: 'app-customViewCell',
  templateUrl: './customViewCell.component.html',
  styleUrls: ['./customViewCell.component.scss']
})
export class CustomViewCellComponent implements ViewCell, OnInit {

  @Input() value: any;    // This hold the cell value
  @Input() rowData: any;  // This holds the entire row object
  constructor() { }

  ngOnInit() {
  }

}
