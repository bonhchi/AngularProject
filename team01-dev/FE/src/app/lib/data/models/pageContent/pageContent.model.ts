import { BaseModel } from '../common';
import { FileDtoModel } from '../files/file.model';

export interface PageContentModel extends BaseModel {
  title: string;
  shortDes: string;
  description: string;
  imageUrl: string;
  order: number;
  files: FileDtoModel[];
}
