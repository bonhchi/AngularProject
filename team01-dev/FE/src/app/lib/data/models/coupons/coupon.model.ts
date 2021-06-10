import { BaseModel } from '../common';

export interface CouponModel extends BaseModel {
  code: string;
  name: string;
  hasPercent: boolean;
  value: number;
  startDate: Date;
  endDate: Date;
}
