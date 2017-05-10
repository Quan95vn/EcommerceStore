using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Web.ViewModels
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        public ContactDetailViewModel ContactDetail { get; set; }
    }
}