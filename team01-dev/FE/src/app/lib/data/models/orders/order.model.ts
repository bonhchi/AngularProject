export class OrderModel {
  id?: string;
  fullName?: string;
  firstName? = '';
  lastName? = '';
  address = '';
  phone = '';
  email = '';
  note? = '';
  status: string = 'Mới';
  totalAmount = 0;
  totalItem = 0;
  orderDetails?: OrderDetailModel[] = [];
  couponId?: string;
  couponName?: string;
  couponCode?: string;
  couponPercent?: number;
  couponValue?: number;
  hasCoupon?: boolean;
  createByDate?: string;
}

export class OrderDetailModel {
  // orderId:string;
  productName = '';
  productId = '';
  price = 0;
  quantity = 0;
  totalAmount = 0;
  productImgUrl = '';
}
