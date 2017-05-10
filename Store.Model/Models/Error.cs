using System;

namespace Store.Model.Models
{
    public class Error
    {
        public int Id { set; get; }

        public string Message { set; get; }

        public string StackTrace { set; get; }

        public DateTime CreatedDate { set; get; }
    }
}
