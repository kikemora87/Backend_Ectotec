namespace Maqueta_Backend_EctoTec.Middleware
{
    public class ErrorDetails
    {
        public ErrorDetails()
        {
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; internal set; }
    }
}
