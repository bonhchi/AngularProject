import { FileDtoModel } from 'src/app/lib/data/models';

export class ModalHeaderModel {
  public title = '';
  public color = '';
  constructor() {}
}

export class ModalFooterModel {
  public title = '';
  public color = '';
  public buttons: ModalButtonModel[];
  constructor() {}
}

export class ModalButtonModel {
  public title = '';
  public color = '';
  public onAction: any;
  constructor() {}
}

export class ModalFile {
  public listFile: FileDtoModel[];
  public entityType: EntityType;
  public entityId = '';
  public typeFile: TypeFile;
  public multiBoolean: boolean;
  constructor() {}
}

export enum TypeFile {
  IMAGE = '.jpg, .jpeg, .png, .icon',
  FILE = '.jpg, .jpeg, .png, .icon, .doc, .docx, .xls, .xlsx, .pdf, .pptx, .ppt, .txt',
}

export enum EntityType {
  BANNER = 'banner',
  CATEGORY = 'category',
  FILE = 'file',
  PRODUCT = 'product',
  SOCIALMEDIA = 'socialmedia',
  USER = 'user',
  BLOG = 'blog',
}
