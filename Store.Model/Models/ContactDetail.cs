namespace Store.Model.Models
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public bool Status { get; set; }

       
    }
}
