import { bind } from "mousetrap";

export class CommonShare {}

export enum ETypePositionInformation {
  BOX_1_4 = "product-detail", //left
  BOX_2 = "product-detail text-center",
  BOX_3_5 = "product-info",
}

export enum ETypePositionCart {
  BOX_1_3 = "cart-info cart-wrap", //right
  BOX_2 = "cart-box",
  BOX_4 = "cart-info",
  BOX_5 = "cart-detail",
}

export enum ETypeSizeImage {
  SMALL = "height: 112.51px; width: 160px;",
  NORMAL = "height: 180px; width: 260px;",
  MEDIUM = "height: 255px; width: 360px;",
  LARGE = "height: 300px; width: 700px;",
}

export enum ETypeGridLayout {
  VERYSMALL = "col-lg-12",
  SMALL = "col-lg-2",
  NORMAL = "col-lg-3",
  MEDIUM = "col-lg-4",
  LARGE = "col-lg-6",
}

export enum ETypeLayoutView {
  GRIDVIEW = "grid-view",
  LISTVIEW = "list-view",
}

export enum TypeDisplayImage {
  BLOG_IMAGE,
  HOME_PRODUCT_IMAGE,
  BLOG_DETAIL,
  LOGO_IMAGE,
  WISHLIST_IMAGE,
  SETTING_IMAGE,
  COMMENT_IMAGE_CUSTOMER,
  PRODUCT_DETAIL_IMAGE,
  PRODUCT_DETAIL_MAIN_IMAGE,
  CART_IMAGE_PRODUCT,
  LOGO_IMAGE_FOOTER,
  ICON_IMAGE_FOOTER,
  CART_MODAL_IMAGE,
  CART_MODAL_BIG_IMAGE,
  LOGO_PAGE_CONTENT,
  CART_DROPDOWN,
  QUICK_VIEW_IMAGE,
}

export const TemplateType = {
  Display: {
    [TypeDisplayImage.BLOG_IMAGE]: "blogImage",
    [TypeDisplayImage.HOME_PRODUCT_IMAGE]: "home-product-image",
    [TypeDisplayImage.BLOG_DETAIL]: "blog-detail",
    [TypeDisplayImage.LOGO_IMAGE]: "logo-image",
    [TypeDisplayImage.WISHLIST_IMAGE]: "wish-list-image",
    [TypeDisplayImage.SETTING_IMAGE]: "setting-image",
    [TypeDisplayImage.COMMENT_IMAGE_CUSTOMER]: "customer-comment-image",
    [TypeDisplayImage.PRODUCT_DETAIL_IMAGE]: "product-details",
    [TypeDisplayImage.PRODUCT_DETAIL_MAIN_IMAGE]: "product-details-main",
    [TypeDisplayImage.CART_IMAGE_PRODUCT]: "cart-image",
    [TypeDisplayImage.LOGO_IMAGE_FOOTER]: "logo-image-footer",
    [TypeDisplayImage.CART_MODAL_IMAGE]: "cart-modal-image",
    [TypeDisplayImage.CART_MODAL_BIG_IMAGE]: "cart-modal-big-image",
    [TypeDisplayImage.LOGO_PAGE_CONTENT]: "logo-page-content",
    [TypeDisplayImage.CART_DROPDOWN]: "cart-dropdown",
    [TypeDisplayImage.ICON_IMAGE_FOOTER]: "icon-image-footer",
    [TypeDisplayImage.QUICK_VIEW_IMAGE]: "quick-view-image",
  },
};
