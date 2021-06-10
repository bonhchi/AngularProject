import { BaseModel } from "../common";
export interface CategoryModel extends BaseModel {
  name: string;
  description: string;
  imageUrl: string;
}
