using System.Collections.Generic;
namespace HeimdallApi.Entities
{
    public class ApiObjectResponse<T> where T: class
    {
        public T Data;
        public string Message;
    }
}
