import { BaseModel } from "../common";
import { baseDTO } from "./baseDTO.model";
import { FileDtoModel } from "../files/file.model";
export interface CategoryModel extends BaseModel, baseDTO{
  name: string;
  description: string;
  imageUrl: string;
  files: FileDtoModel[];
}
