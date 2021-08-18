namespace Infrastructure.Constants
{
    public struct SystemMessage
    {
        public static readonly Message UnAuthorized = new() { Code = 401, Content = "Unauthorized" };
        public static readonly Message Forbidden = new() { Code = 403, Content = "Forbidden" };
        public static readonly Message ErrorSystem = new() { Code = 500, Content = "Somethings went wrong!" };
        public static readonly Message DataNotFound = new() { Code = 404, Content = "Data not found" };
        public static readonly Message CreateSuccess = new() { Code = 200, Content = "Create success" };
        public static readonly Message UpdateSuccess = new() { Code = 2001, Content = "Update successfully" };
    }
    public class Message
    {
        public string Content { get; set; }
        public int Code { get; set; }
    }
}
