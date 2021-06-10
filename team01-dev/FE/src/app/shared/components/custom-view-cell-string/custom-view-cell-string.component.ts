import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-custom-view-cell-string',
  templateUrl: './custom-view-cell-string.component.html',
  styleUrls: ['./custom-view-cell-string.component.scss']
})

export class CustomViewCellStringComponent implements OnInit {
  
  @Input() value: any;    // This hold the cell value
  @Input() rowData: any;  // This holds the entire row object
  
  constructor() { }

  ngOnInit() {
  }

}
