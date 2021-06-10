import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from "@angular/core";
import {
  CategoryModel,
  PageModel,
  ReturnMessage,
} from "src/app/lib/data/models";
import { ProductListService } from "src/app/lib/data/services";
import { Product } from "../../classes/product";

@Component({
  selector: "app-categories",
  templateUrl: "./categories.component.html",
  styleUrls: ["./categories.component.scss"],
  providers: [ProductListService],
})
export class CategoriesComponent implements OnInit {
  public products: Product[] = [];
  public collapse: boolean = true;
  public categories: CategoryModel[] = [];

  event: any = {};
  @Input() categoryName: string[] = [];
  @Input() styleFont: string = "color: black; font-weight: bold";
  styleFontNormal: string = "color: #77777777; font-weight: normal";
  // [style]="category.name.indexOf(categoryName) > -1?styleFont:styleFontNormal"
  @Output() onChangeTypeCate = new EventEmitter();

  constructor(
    public productListService: ProductListService,
    private elRef: ElementRef
  ) {
    this.productListService
      .getCategory()
      .then((res: ReturnMessage<CategoryModel[]>) => {
        this.categories = res.data;
      });
  }

  ngOnInit(): void {}

  get filterbyCategory() {
    const category = this.categories;
    return category;
  }

  onSelect(event, typeCate: string) {
    this.onChangeTypeCate.emit(typeCate);
  }

  bigImg(event) {
    this.event.color = event.color;
    this.event["font-weight"] = event["font-weight"];

    event.color = "black";
    event["font-weight"] = "bold";
  }

  normalImg(event) {
    if (this.event) {
      event.color = this.event.color;
      event["font-weight"] = this.event["font-weight"];
    }
  }
}
