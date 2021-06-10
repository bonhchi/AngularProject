import { BaseModel } from '../common';

export interface BlogModel extends BaseModel {
  title: string;
  shortDes: string;
  contentHTML: string;
  imageUrl: string;
}
