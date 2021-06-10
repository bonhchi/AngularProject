import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  TemplateRef,
  Input,
  Injectable,
  PLATFORM_ID,
  Inject,
  Output,
  EventEmitter,
} from "@angular/core";
import { isPlatformBrowser } from "@angular/common";
import { NgbModal, ModalDismissReasons } from "@ng-bootstrap/ng-bootstrap";
import { Router } from "@angular/router";
import { AuthService, FileService, MessageService } from "src/app/lib/data/services";
import { ProductModel, UserDataReturnDTOModel } from "src/app/lib/data/models";
import { HomeService } from "src/app/lib/data/services/home/home.service";
import { ETypeSizeImage, TypeDisplayImage } from "src/app/shared/data";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import { SaveCustomerWishListModel } from "src/app/lib/data/models/customerWishList/customerWishList.model";
import { CustomerWishListService } from "src/app/lib/data/services/customerWishLists/customerWishList.service";
import { Subscription } from "rxjs";
import { CartModalComponent } from "../../modal/cart-modal/cart-modal.component";

@Component({
  selector: "app-quick-view",
  templateUrl: "./quick-view.component.html",
  styleUrls: ["./quick-view.component.scss"],
  providers: [],
  styles: [
    `
      .star {
        position: relative;
        display: inline-block;
        font-size: 2.5rem;
        color: #d3d3d3;
      }
      .full {
        color: #ffa200;
      }
      .half {
        position: absolute;
        display: inline-block;
        overflow: hidden;
        color: #ffa200;
      }
    `,
  ],
})
export class QuickViewComponent implements OnInit, OnDestroy {
  @Input() product: ProductModel;
  @Input() currency: any;
  @Input() cartModal: boolean = false; // Default False
  @ViewChild("quickView", { static: false }) QuickView: TemplateRef<any>;
  @ViewChild("cartModalQuickView") CartModal: CartModalComponent;
  @Output() isCloseModal: EventEmitter<any> = new EventEmitter<any>();

  public closeResult: string;
  public ImageSrc: string;
  public counter: number = 1;
  public modalOpen: boolean = false;
  public thumbnail: boolean = true;
  public onHowerChangeImage: boolean = true;
  public typeSizeImage = ETypeSizeImage.MEDIUM;

  userInfo: UserDataReturnDTOModel;
  subDataUser: Subscription;
  typeDisplayImage = TypeDisplayImage;


  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private router: Router,
    private modalService: NgbModal,
    private homeService: HomeService,
    private cartService: CartService,
    private messageService: MessageService,
    private authService: AuthService,
    private wishListService: CustomerWishListService,
  ) { }

  ngOnInit(): void {
    // console.log(this.product);
    this.ImageSrc = this.product.imageUrl.split(",")[0];
    this.subDataUser = this.authService.callUserInfo.subscribe((it) => {
      this.userInfo = it;
    });
  }

  openModal() {
    this.modalOpen = true;
    if (isPlatformBrowser(this.platformId)) {
      // For SSR
      this.modalService
        .open(this.QuickView, {
          size: "lg",
          ariaLabelledBy: "modal-basic-title",
          centered: true,
          windowClass: "Quickview",
        })
        .result.then(
          (result) => {
            `Result ${result}`;
          },
          (dismiss) => {
          }
        );
    }
  }
  ChangeVariantsImage(src) {
    this.ImageSrc = src;
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

  // Get Product Color
  Color(variants) {
    const uniqColor = [];
    for (let i = 0; i < Object.keys(variants).length; i++) {
      if (uniqColor.indexOf(variants[i].color) === -1 && variants[i].color) {
        uniqColor.push(variants[i].color);
      }
    }
    return uniqColor;
  }

  // Get Product Size
  Size(variants) {
    const uniqSize = [];
    for (let i = 0; i < Object.keys(variants).length; i++) {
      if (uniqSize.indexOf(variants[i].size) === -1 && variants[i].size) {
        uniqSize.push(variants[i].size);
      }
    }
    return uniqSize;
  }

  // Change Variants
  ChangeVariants(color, product) {
    product.variants.map((item) => {
      if (item.color === color) {
        product.images.map((img) => {
          if (img.image_id === item.image_id) {
            this.ImageSrc = img.src;
          }
        });
      }
    });
  }

  // Increament
  increment() {
    this.counter++;
  }

  // Decrement
  decrement() {
    if (this.counter > 1) this.counter--;
  }

  // Add to cart
  async addToCart(product: any) {
    this.openModelCart(product);
    this.modalService.dismissAll();
  }

  ngOnDestroy() {
    if (this.modalOpen) {
      this.modalService.dismissAll();
    }
    this.subDataUser.unsubscribe();
    this.subDataUser = null;
  }

  getImage(fileName: string) {
    return FileService.getLinkFile(fileName);
  }

  addToWishlist(product: any) {
    const model: SaveCustomerWishListModel = {
      productId: product.id,
    };
    if (this.product.isInWishList) {
      return this.messageService
        .confirm("Remove in wish list?")
        .then((result) => {
          if (result.isConfirmed) {
            this.wishListService.createOrDelete(model).then(() => {
              this.product.isInWishList = false;
              this.cartService.removeWishlistItem(product);
            });
          }
        });
    }
    this.wishListService.createOrDelete(model).then(() => {
      this.product.isInWishList = true;
      this.cartService.addToWishlist(product);
    });
  }

  openModelCart(product: ProductModel) {
    this.CartModal.openModal(product)
    this.isCloseModal.emit(true);
  }
}
