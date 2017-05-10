using AutoMapper;
using Store.Model.Models;
using Store.Web.ViewModels;

namespace Store.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>().MaxDepth(2);
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>().MaxDepth(2);
                cfg.CreateMap<Category, CategoryViewModel>().MaxDepth(2);
                cfg.CreateMap<ContactDetail, ContactDetailViewModel>().MaxDepth(2);
                cfg.CreateMap<Feedback, FeedbackViewModel>().MaxDepth(2);
                cfg.CreateMap<Order, OrderViewModel>().MaxDepth(2);
                cfg.CreateMap<OrderDetail, OrderDetailViewModel>().MaxDepth(3);
                cfg.CreateMap<Product, ProductViewModel>().MaxDepth(3);
                cfg.CreateMap<Slide, SlideViewModel>().MaxDepth(2);
            });  
        }
    }
}