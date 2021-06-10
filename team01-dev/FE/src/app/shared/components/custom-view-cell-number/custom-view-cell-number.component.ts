import { Component, Input, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';

@Component({
  selector: 'app-custom-view-cell-number',
  templateUrl: './custom-view-cell-number.component.html',
  styleUrls: ['./custom-view-cell-number.component.scss']
})
export class CustomViewCellNumberComponent implements ViewCell, OnInit {

  @Input() value: any;    // This hold the cell value
  @Input() rowData: any;  // This holds the entire row object
  
  constructor() { }

  ngOnInit() {
  }

}
