namespace Infrastructure.Constants
{
    public struct SystemMessage
    {
        public static readonly Message UnAuthorized = new Message { Code = 401, Content = "Unauthorized" };
        public static readonly Message Forbidden = new Message { Code = 403, Content = "Forbidden" };
        public static readonly Message ErrorSystem = new Message { Code = 500, Content = "Somethings went wrong!" };
        public static readonly Message DataNotFound = new Message { Code = 404, Content = "Data not found" };
        public static readonly Message CreateSuccess = new Message { Code = 200, Content = "Create success" };
        public static readonly Message UpdateSuccess = new Message { Code = 2001, Content = "Update successfully" };
    }

    public class Message
    {
        public string Content { get; set; }
        public int Code { get; set; }
    }
}
