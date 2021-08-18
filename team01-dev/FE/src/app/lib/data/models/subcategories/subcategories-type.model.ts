import { BaseModel } from 'src/app/lib/data/models';
import { baseDTO } from 'src/app/lib/data/models/categories/baseDTO.model';

export interface SubcategoryTypeModel extends BaseModel, baseDTO {
  name: string;
  englishName: string;
}
