import { CategoryModel } from "../categories/category.model";
import { InformationWebModel } from "../informationweb/informationweb.model";
import { PageContentModel } from "../pageContent/pageContent.model";
import { SocialMediaModel } from "../social-medias/social-media.model";

export interface FooterModel {
    categories: CategoryModel[];
    socialMedias: SocialMediaModel[];
    pageContents : PageContentModel[];
    informationWeb: InformationWebModel;
}