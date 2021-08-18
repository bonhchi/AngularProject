import { CategoryModel } from "../categories/category.model";
import { InformationWebModel } from "../information-web/information-web.model";
import { PageContentModel } from "../page-content/page-content.model";
import { SocialMediaModel } from "../social-medias/social-media.model";

export interface FooterModel {
  categories: CategoryModel[];
  socialMedias: SocialMediaModel[];
  pageContents: PageContentModel[];
  informationWeb: InformationWebModel;
}
