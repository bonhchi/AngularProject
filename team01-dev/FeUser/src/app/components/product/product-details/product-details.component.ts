import { Component, OnInit, ViewChild } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {
  CommentModel,
  CreateCommentModel,
} from "src/app/lib/data/models/comments/comment.model";
import { ProductDetailsModel } from "src/app/lib/data/models/products/product-details.model";
import { UserDataReturnDTOModel } from "src/app/lib/data/models/users/user.model";
import {
  AuthService,
  FileService,
  MessageService,
} from "src/app/lib/data/services";
import { ProductDetailsService } from "src/app/lib/data/services/products/product-details.service";
import { SizeModalComponent } from "src/app/shared/components/modal/size-modal/size-modal.component";
import {
  ProductDetailsMainSlider,
  ProductDetailsThumbSlider,
} from "src/app/shared/data/slider";

import { PageModel, ReturnMessage } from "src/app/lib/data/models";
import { ETypeSizeImage, TypeDisplayImage } from "src/app/shared/data";
import { CommentService } from "src/app/lib/data/services/comments/comment.service";
import { Subscription } from "rxjs";
import { CartService } from "src/app/lib/data/services/cart/cart.service";
import { ToastrService } from "ngx-toastr";
import { SaveCustomerWishListModel } from "src/app/lib/data/models/customerWishList/customerWishList.model";
import { CustomerWishListService } from "src/app/lib/data/services/customerWishLists/customerWishList.service";

@Component({
  selector: "app-product-details",
  templateUrl: "./product-details.component.html",
  styleUrls: ["./product-details.component.scss"],
  providers: [
    ProductDetailsService,
    CommentService,
    CartService,
    CustomerWishListService,
  ],
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
export class ProductDetailsComponent implements OnInit {
  @ViewChild("sizeChart") SizeChart: SizeModalComponent;
  public product: ProductDetailsModel;
  public counter: number = 1;
  public activeSlide: any = 0;
  public ImageSrc: string;
  public dataComment: CreateCommentModel;
  public typeDisplayImage = TypeDisplayImage;
  public typeSizeImage = ETypeSizeImage;
  public user: UserDataReturnDTOModel;
  public comments: PageModel<CommentModel>;
  public searchModel: any;
  public ProductDetailsMainSliderConfig: any = ProductDetailsMainSlider;
  public ProductDetailsThumbConfig: any = ProductDetailsThumbSlider;

  subDataUser: Subscription;

  public currentRate: number;
  public token: string;
  public item: any;

  public rating: number;

  constructor(
    private productDetailsService: ProductDetailsService,
    private activatedRoute: ActivatedRoute,
    private commentService: CommentService,
    private cartService: CartService,
    private toastrService: ToastrService,
    private authService: AuthService,
    private wishListService: CustomerWishListService,
    private sweetService: MessageService
  ) {}

  ngOnInit(): void {
    this.token = localStorage.getItem("token");
    this.subDataUser = this.authService.callUserInfo.subscribe(
      (it) => (this.user = it)
    );

    this.initDataComment();
    this.getProduct();
  }

  getProduct() {
    this.activatedRoute.queryParams.subscribe((param) => {
      this.productDetailsService
        .get(param.id)
        .then((res: ReturnMessage<ProductDetailsModel>) => {
          this.product = res.data;
        });
      this.createSearchModel();
      this.getComments();
    });
  }

  getImage(fileName: string) {
    return FileService.getLinkFile(fileName);
  }

  initDataComment() {
    this.dataComment = {
      entityId: this.activatedRoute.snapshot.queryParamMap.get("id"),
      entityType: "Product",
      rating: 5,
    };
  }

  createSearchModel() {
    const id = this.activatedRoute.snapshot.queryParamMap.get("id");
    this.searchModel = {
      ["search.entityId"]: id,
      ["pageIndex"]: 0,
      ["pageSize"]: 10,
    };
  }

  changePageIndex(pageIndex: number) {
    this.searchModel = { ...this.searchModel, ["pageIndex"]: pageIndex - 1 };

    this.getComments();
  }

  getComments() {
    this.commentService
      .getProductComments(this.searchModel)
      .then((data: ReturnMessage<PageModel<CommentModel>>) => {
        this.comments = data.data;
      })
      .catch((e) => {});
  }
  addToCart(product: any) {
    this.cartService.addToCart(product);
    this.sweetService.notification(
      "Sản phẩm đã được thêm vào giỏ hàng",
      "success"
    );
  }

  addToWishList(product) {
    const model: SaveCustomerWishListModel = {
      productId: product.id,
    };
    if (this.product.isInWishList) {
      return this.sweetService
        .confirm("Xóa khỏi danh sách yêu thích")
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
}
