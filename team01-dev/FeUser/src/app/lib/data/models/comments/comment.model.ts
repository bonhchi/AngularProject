import { BaseModel } from "../common";

export interface CommentModel extends BaseModel {
  fullName: string;
  customerId: string;
  entityId: string;
  entityType: string;
  content: string;
  rating: number;
}

export interface SearchCommentModel {
  fullName?: string;
  customerId?: string;
  entityId?: string;
  entityType?: string;
  content?: string;
  rating?: number;
}

export interface CreateCommentModel {
  entityId: string;
  entityType: string;
  rating: number;
}
