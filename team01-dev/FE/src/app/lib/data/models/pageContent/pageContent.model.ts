import { FileDtoModel } from 'src/app/components/models/files/file.model';
import { BaseModel } from '../common';

export interface PageContentModel extends BaseModel {
  title: string;
  shortDes: string;
  description: string;
  imageUrl: string;
  order: number;
  files: FileDtoModel[];
}
