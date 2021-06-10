import { BaseModel } from '../common';
import { FileDtoModel } from '../files/file.model';

export interface SocialMediaModel extends BaseModel {
  title: string;
  link: string;
  iconUrl: string;
  displayOrder: number;
  files: FileDtoModel[];
}
