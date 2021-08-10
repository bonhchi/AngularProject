import { BaseModel } from 'src/app/lib/data/models';
import { baseDTO } from 'src/app/lib/data/models/categories/baseDTO.model';

export interface SubcategoryModel extends BaseModel, baseDTO {
  name: string;
  subcategoryTypeId: string;
  categoryId: string;
  subcategoryTypeName: string;
  categoryName: string;
}
