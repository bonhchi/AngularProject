import { BaseModel } from "../common";
// import { FileDtoModel } from "../files/file.model";
export class OrderModel {
  id?: string;
  fullName?: string;
  firstName = "";
  lastName = "";
  address = "";
  phone = "";
  email = "";
  status: "New";
  totalAmount = 0;
  totalItem = 0;
  orderDetails?: OrderDetailModel[] = [];
  couponId?: string;
  couponName?: string;
  couponCode?: string;
  couponPercent?: number;
  couponValue?: number;
}

export class OrderDetailModel {
  orderId: string;
  id: string;
  productName = "";
  productId = "";
  price = 0;
  quantity = 0;
  totalAmount = 0;
}
