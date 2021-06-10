import { ProductModel } from "..";

export interface CartModel {
  cartDetails:ProductModel[];
  totalAmount:number;
}
