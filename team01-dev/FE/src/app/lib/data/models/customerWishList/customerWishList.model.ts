import { BaseModel } from '../common';

export interface CustomerWishListModel extends BaseModel {
  customerId: string;
  productId: string;
}
