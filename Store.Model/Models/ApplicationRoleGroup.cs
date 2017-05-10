namespace Store.Model.Models
{
    public class ApplicationRoleGroup
    {
        public int GroupId { set; get; }
        public string RoleId { set; get; }


        public virtual ApplicationRole ApplicationRole { set; get; }
        public virtual ApplicationGroup ApplicationGroup { set; get; }
    }
}
