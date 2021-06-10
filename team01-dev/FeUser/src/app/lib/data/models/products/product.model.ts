import { BaseModel } from "../common";

export interface ProductModel extends BaseModel {
  name: string;
  description: string;
  imageUrl: string;
  contentHTML: string;
  displayOrder: number;
  categoryId: number;
  categoryName: string;
  price: number;
  ratingScore: number;
  isFeatured: boolean;
  quantity?: number;
  saleCount: number;
  isInWishList?: boolean;
}

export enum ETypeSort {
  NULL,
  AZ,
  ZA,
  PRICELOW,
  PRICEHIGH,
}
