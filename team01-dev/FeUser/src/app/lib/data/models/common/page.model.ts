export class PageModel<T> {
  pageIndex: number;
  totalItem: number;
  totalPage: number;
  pageSize: number;
  results: T[];
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export class SearchPaganationDTO<T> {
  search: T;
  pageIndex: number;
  pageSize: number;
}
