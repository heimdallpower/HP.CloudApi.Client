using System.Collections.Generic;
namespace HeimdallApi.Entities
{
    //todo check if we can use either ApiObjectResponse or ApiResponse
    public class ApiResponse<T>
    {
        public List<T> Data = new List<T>();
        public string Message;
    }
}
