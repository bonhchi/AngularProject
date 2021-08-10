import { Component, OnInit } from '@angular/core';
import { SubcategoryModel } from '../../../lib/data/models/subcategories/subcategories.model';

@Component({
  selector: 'app-list-subcategories',
  templateUrl: './list-subcategories.component.html',
  styleUrls: ['./list-subcategories.component.scss'],
})
export class ListSubcategoriesComponent implements OnInit {
  public subcategories: SubcategoryModel[];

  constructor() {}

  ngOnInit() {}
}
