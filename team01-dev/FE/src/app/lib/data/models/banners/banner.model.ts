import { BaseModel } from '../common';
import { FileDtoModel } from '../files/file.model';
export interface BannerModel extends BaseModel {
  title: string;
  description: string;
  link: string;
  imageUrl: string;
  displayOrder: number;
  files: FileDtoModel[];
}
