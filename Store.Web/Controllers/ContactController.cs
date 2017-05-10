using AutoMapper;
using BotDetect.Web.Mvc;
using FluentValidation.Results;
using Store.Common;
using Store.Model.Models;
using Store.Service;
using Store.Web.Infrastructure.Extension;
using Store.Web.Infrastructure.Validators;
using Store.Web.ViewModels;
using System.Web.Mvc;

namespace Store.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDetailService _contactDetailService;
        private readonly IFeedbackService _feedBackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedBackService)
        {
            _contactDetailService = contactDetailService;
            _feedBackService = feedBackService;
        }

        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();

            return View(viewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var contactDetail = _contactDetailService.GetDefaultContact();
            var contactViewModel = MapDataToVm(contactDetail);

            return contactViewModel;
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        [ValidateAntiForgeryToken]
        public ActionResult SendFeedBack(FeedbackViewModel feedBackViewModel, bool captchaValid)
        {
            var varlidator = new FeedbackViewModelValidator();
            var result = varlidator.Validate(feedBackViewModel);

            if ((!result.IsValid && !captchaValid) || (!result.IsValid || !captchaValid) )
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }
                ModelState.AddModelError("captcha-error","");
                feedBackViewModel.ContactDetail = GetDetail();

                return View("Index",feedBackViewModel);
            }
            else
            {
                SendFeedBack(feedBackViewModel);
                SendEmail(feedBackViewModel);
            }
           
            return RedirectToAction("Index");
        }

        private void SendFeedBack(FeedbackViewModel feedBackViewModel)
        {
            var feedback = new Feedback();
            feedback.UpdateFeedback(feedBackViewModel);
            _feedBackService.Add(feedback);
            _feedBackService.Save();

            TempData["SuccessMsg"] = "Gửi phản hồi thành công";
        }

        private ContactDetailViewModel MapDataToVm(ContactDetail contactDetail)
        {
            return Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
        }

        private void SendEmail(FeedbackViewModel feedBackViewModel)
        {
            string contend = System.IO.File.ReadAllText(Server.MapPath("/Assets/Client/template/contact_template.html"));
            contend = contend.Replace("{{Name}}", feedBackViewModel.Name);
            contend = contend.Replace("{{Email}}", feedBackViewModel.Email);
            contend = contend.Replace("{{Message}}", feedBackViewModel.Message);

            var adminEmail = ConfigHelper.GetByKey("AdminEmail");

            MailHelper.SendMail(adminEmail, "Thông tin liên hệ", contend);
        }
    }
}