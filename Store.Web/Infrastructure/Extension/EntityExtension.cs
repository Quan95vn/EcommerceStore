using Store.Model.Models;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Web.Infrastructure.Extension
{
    public static class EntityExtension
    {
        public static void UpdateCategory(this Category category, CategoryViewModel categoryVm)
        {
            category.Id = categoryVm.Id;
            category.Name = categoryVm.Name;
            category.Alias = categoryVm.Alias;
            category.ParentId = categoryVm.ParentId;
            category.DisplayOrder = categoryVm.DisplayOrder;

            category.CreatedDate = categoryVm.CreatedDate;
            category.CreatedBy = categoryVm.CreatedBy;
            category.UpdatedDate = categoryVm.UpdatedDate;
            category.UpdatedBy = categoryVm.UpdatedBy;
            category.MetaKeyword = categoryVm.MetaKeyword;
            category.MetaDescription = categoryVm.MetaDescription;
            category.Status = categoryVm.Status;
        }

        public static void UpdateContactDetail(this ContactDetail contactDetail, ContactDetailViewModel contactDetailVm)
        {
            contactDetail.Id = contactDetailVm.Id;
            contactDetail.Name = contactDetailVm.Name;
            contactDetail.Email = contactDetailVm.Email;
            contactDetail.PhoneNumber = contactDetailVm.PhoneNumber;
            contactDetail.Message = contactDetailVm.Message;
            contactDetail.Address = contactDetailVm.Address;
            contactDetail.Website = contactDetailVm.Website;
            contactDetail.Lat = contactDetailVm.Lat;
            contactDetail.Lng = contactDetailVm.Lng;
            contactDetail.Status = contactDetailVm.Status;

        }

        public static void UpdateFeedback(this Feedback feedBack, FeedbackViewModel feedBackVm)
        {
            feedBack.Id = feedBackVm.Id;
            feedBack.Name = feedBackVm.Name;
            feedBack.Email = feedBackVm.Email;
            feedBack.PhoneNumber = feedBackVm.PhoneNumber;
            feedBack.Message = feedBackVm.Message;
            feedBack.CreatedDate = DateTime.Now;
        }




        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.Id = productVm.Id;
            product.Name = productVm.Name;
            product.Alias = productVm.Alias;
            product.ThumbImage = productVm.ThumbImage;
            product.MoreImages = productVm.MoreImages;
            product.OriginalPrice = productVm.OriginalPrice;
            product.Price = productVm.Price;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Warranty = productVm.Warranty;
            product.Description = productVm.Description;
            product.Content = productVm.Content;
            product.InfoDetail = productVm.InfoDetail;
            product.Tags = productVm.Tags;
            product.HotFlag = productVm.HotFlag;
            product.IsFeature = productVm.IsFeature;
            product.CategoryId = productVm.CategoryId;
            product.Quantity = productVm.Quantity;
            product.ViewCount = productVm.ViewCount;

            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.MetaKeyword = productVm.MetaKeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Status = productVm.Status;
        }

        public static void UpdateOrder(this Order order, OrderViewModel orderVm)
        {
            order.CustomerName = orderVm.CustomerName;
            order.CustomerAddress = orderVm.CustomerAddress;
            order.CustomerEmail = orderVm.CustomerEmail;
            order.CustomerMobile = orderVm.CustomerMobile;
            order.CustomerMessage = orderVm.CustomerMessage;
            order.PaymentStatus = orderVm.PaymentStatus;


            order.CreatedDate = DateTime.Now;
            order.CreatedBy = orderVm.CreatedBy;
            order.Status = false;
            order.CustomerId = orderVm.CustomerId;
        }

        public static void UpdateSlide(this Slide slide, SlideViewModel slideVm)
        {
            slide.Id = slideVm.Id;
            slide.Name = slideVm.Name;
            slide.Image = slideVm.Image;
            slide.DisplayOrder = slideVm.DisplayOrder;
            slide.Status = slideVm.Status;
        }

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.Id = appGroupViewModel.Id;
            appGroup.Name = appGroupViewModel.Name;
            appGroup.Description = appGroupViewModel.Description;
        }

        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {

            appUser.UserName = appUserViewModel.UserName;
            appUser.FullName = appUserViewModel.FullName;
            appUser.Email = appUserViewModel.Email;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
            appUser.Address = appUserViewModel.Address;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();

            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }
    }
}