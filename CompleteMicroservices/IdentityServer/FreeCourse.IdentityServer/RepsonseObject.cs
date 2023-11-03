using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public class ResponseObject<T>
    {
        public T Data { get; private set; }
        public int StatusCode { get; private set; }
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; set; }
        public ResponseObject(T data, int statusCode, bool isSuccessful, List<string> errors)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccessful = isSuccessful;
            Errors = errors;
        }
        public ResponseObject(T data, int statusCode, bool isSuccessful)
        {
            Data = data;
            StatusCode = statusCode;
            IsSuccessful = isSuccessful;
        }
    }
}
