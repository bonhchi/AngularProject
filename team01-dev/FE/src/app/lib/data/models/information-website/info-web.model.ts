import { FileDtoModel } from "..";
import { baseDTO } from "../categories/baseDTO.model";
import { BaseModel } from "../common";


export interface InformationWebModel extends BaseModel, baseDTO{
    address: string; 
    phone: string;
    email: string;
    fax: string;
    logo: string;
    title: string;
    description: string;
    files: FileDtoModel[];
}