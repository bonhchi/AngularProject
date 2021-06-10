import { BaseModel } from "../common";
export interface BannerModel extends BaseModel {
  title: string;
  description: string;
  link: string;
  imageUrl: string;
  displayOrder: number;
}
