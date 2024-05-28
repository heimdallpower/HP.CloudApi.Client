namespace HeimdallPower.Entities
{
    public class ApiResponse<T> where T : class
    {
        public T Data;
        public string Message;
    }
}
