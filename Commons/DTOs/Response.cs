
namespace Commons.DTOs
{
    public class Response<T>
    {

        public dynamic status { get; set; }

        public T data { get; set; }

        public string message { get; set; }

    }
}
