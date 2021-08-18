import { BaseModel } from "../common";

export interface PageContentModel extends BaseModel {
  title: string;
  order: number;
  shortDes: string;
  description: string;
  imageUrl: string;
}

export interface PageContentInfoModel extends BaseModel {
  address: string;
  phone: string;
  email: string;
  fax: string;
  logo: string;
}
