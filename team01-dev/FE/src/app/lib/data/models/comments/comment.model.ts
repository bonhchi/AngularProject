import { BaseModel } from '../common';

export interface CommentModel extends BaseModel {
  fullName: string;
  customerId: string;
  entityId: string;
  entityType: string;
  content: string;
}
