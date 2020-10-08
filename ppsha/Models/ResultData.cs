
namespace ppsha.Models
{
    public class ResultData
    {
        public string Status { get; set; } // Ok / Error
        public object Data { get; set; }  // result data
        public string Message { get; set; } // error message / sucess message or null
    }
}
