import { baseDTO } from "../categories/baseDTO.model";
import { BaseModel } from "../common";
import { FileDtoModel } from "../files/file.model";

export interface ProductModel extends BaseModel, baseDTO{
    name: string,
    description: string,
    imageUrl: string,
    contentHTML: string,
    displayOrder: number,
    hasDisplayHomePage: boolean,
    categoryName: string,
    categoryId: string,
    price: number,
    isFeatured: boolean,
    files: FileDtoModel[],
}