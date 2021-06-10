import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  TemplateRef,
  Input,
  AfterViewInit,
  Injectable,
  PLATFORM_ID,
  Inject,
  Pipe,
} from "@angular/core";
import { isPlatformBrowser } from "@angular/common";
import {
  NgbModal,
  ModalDismissReasons,
  NgbActiveModal,
} from "@ng-bootstrap/ng-bootstrap";
import { ProductModel } from "src/app/lib/data/models/products/product.model";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import {
  FileService,
  MessageService,
  ProductListService,
} from "src/app/lib/data/services";
import { ETypeGridLayout, TypeDisplayImage } from "src/app/shared/data";

@Component({
  selector: "app-cart-modal",
  templateUrl: "./cart-modal.component.html",
  styleUrls: ["./cart-modal.component.scss"],
  providers: [CartService, ProductListService, Pipe],
})
export class CartModalComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() product: ProductModel;
  @Input() currency: any;
  public grid: string = ETypeGridLayout.NORMAL;

  @ViewChild("cartModal", { static: false }) CartModal: TemplateRef<any>;

  public closeResult: string;
  public modalOpen: boolean = false;
  public products: ProductModel[] = [];
  public typeDisplayImage = TypeDisplayImage;

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private modalService: NgbModal,
    private productListService: ProductListService,
    private cartService: CartService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void { }

  async openModal(product) {
    const getData = await this.productListService.getByCategory(
      product.categoryId,
      null
    );
    this.products = getData.data;
    const status = await this.cartService.addToCart(product);
    if (status) {
      this.modalOpen = true;
      if (isPlatformBrowser(this.platformId)) {
        // For SSR
        this.modalService
          .open(this.CartModal, {
            size: "lg",
            ariaLabelledBy: "Cart-Modal",
            centered: true,
            windowClass: "theme-modal cart-modal CartModal",
          })
          .result.then(
            (result) => {
              `Result ${result}`;
            },
            (dismiss) => { }
          );
      }
    }
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return "by pressing ESC";
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return "by clicking on a backdrop";
    } else {
      return `with: ${reason}`;
    }
  }

  ngOnDestroy() {
    if (this.modalOpen) {
      this.modalService.dismissAll();
    }
  }
  getImage(image) {
    return FileService.getLinkFile(image);
  }

  async addToCart(product: any) {
    this.cartService.addToCart(product);
    this.messageService.notification(
      "Sản phẩm đã được thêm vào giỏ hàng",
      "success"
    );
  }

  onCloseModal(event: boolean, modal) {
    if (event) {
      modal.dismiss("Cross click");
    }
  }
}
