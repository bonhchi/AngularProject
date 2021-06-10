import { BlogModel, CategoryModel } from "..";
import { InformationWebModel } from "../informationweb/informationweb.model";

export interface HeaderModel {
	categories: CategoryModel[],
	blogs: BlogModel[],
	informationWeb: InformationWebModel;
}
export interface Menu {
	path?: string;
	title?: string;
	type?: string;
	megaMenu?: boolean;
	image?: string;
	active?: boolean;
	badge?: boolean;
	badgeText?: string;
	children?: Menu[];
}
