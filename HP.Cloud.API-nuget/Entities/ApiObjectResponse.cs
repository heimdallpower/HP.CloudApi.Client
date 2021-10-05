using System.Collections.Generic;
namespace HeimdallPower.Entities
{
    public class ApiObjectResponse<T> where T: class
    {
        public T Data;
        public string Message;
    }
}
