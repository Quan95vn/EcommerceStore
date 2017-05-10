namespace Store.Model.Models
{
    public class Slide
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Image { set; get; }
        public int? DisplayOrder { set; get; }
        public bool? Status { set; get; }
    }
}
