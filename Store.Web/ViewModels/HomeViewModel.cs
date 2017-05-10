using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Web.ViewModels
{
    public class HomeViewModel
    {    
       
        public IEnumerable<SlideViewModel> SlideVm { get; set; }
        public IEnumerable<ProductViewModel> FeatureProductVm { get; set; }
        public IEnumerable<ProductViewModel> HotProductVm { get; set; }
    }
}