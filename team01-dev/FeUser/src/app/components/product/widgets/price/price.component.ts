import {
  Component,
  OnInit,
  Output,
  Input,
  EventEmitter,
  Inject,
  PLATFORM_ID,
  ViewEncapsulation,
} from "@angular/core";
import { isPlatformBrowser } from "@angular/common";
import { LabelType, Options } from "ng5-slider";

@Component({
  selector: "app-price",
  templateUrl: "./price.component.html",
  styleUrls: ["./price.component.scss"],
  encapsulation: ViewEncapsulation.None,
})
export class PriceComponent implements OnInit {
  // Using Output EventEmitter
  @Output() priceFilter: EventEmitter<any> = new EventEmitter<any>();

  // define min, max and range
  @Input() min: number;
  @Input() max: number;

  public collapse: boolean = true;
  public isBrowser: boolean = false;

  public price: any;

  options: Options = {
    floor: 0,
    ceil: 1000000000,
    translate: (value: number, label: LabelType): string => {
      return value.toLocaleString("vn") + "đ";
    },
  };

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    if (isPlatformBrowser(this.platformId)) {
      this.isBrowser = true; // for ssr
    }
  }

  ngOnInit(): void {}

  // Range Changed
  appliedFilter(event: any) {
    this.price = { minPrice: event.value, maxPrice: event.highValue };
    this.priceFilter.emit(this.price);
  }
}
