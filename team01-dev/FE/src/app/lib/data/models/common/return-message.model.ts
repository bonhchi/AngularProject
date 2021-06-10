export interface ReturnMessage<T> {
  hasError: boolean;
  statusCode: string;
  message: string;
  data: T;
}
