namespace Common.Http
{
    public class ReturnMessage<T>
    {

        public bool HasError { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ReturnMessage(bool hasError, T data, string message)
        {
            HasError = hasError;
            StatusCode = hasError ? StatusCodeDefault.Error : StatusCodeDefault.Success;
            Message = message;
            Data = data;
        }
        public ReturnMessage(bool hasError, T data, string statusCode, string message)
        {
            HasError = hasError;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

    }
}
