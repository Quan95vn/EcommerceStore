using System.Collections.Generic;

namespace Store.Web.ViewModels
{
    public class ApplicationGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ApplicationRoleViewModel> Roles { get; set; }
    }
}