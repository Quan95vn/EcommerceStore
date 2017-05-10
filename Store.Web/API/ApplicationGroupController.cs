using AutoMapper;
using Store.Common.Exceptions;
using Store.Model.Models;
using Store.Service;
using Store.Web.App_Start;
using Store.Web.Infrastructure.Core;
using Store.Web.Infrastructure.Extension;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Store.Web.API
{
    [RoutePrefix("api/applicationGroup")]
    [Authorize]
    public class ApplicationGroupController : ApiControllerBase
    {
        private readonly IApplicationGroupService _appGroupService;
        private readonly IApplicationRoleService _appRoleService;
        private ApplicationUserManager _userManager;

        public ApplicationGroupController(IErrorService errorService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IApplicationGroupService appGroupService) : base(errorService)
        {
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
            _userManager = userManager;
        }

        [Route("getlistpaging")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _appGroupService.GetAll(page, pageSize, out totalRow, filter);
                var modelVm = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);

                var pagedSet = new PaginationSet<ApplicationGroupViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Route("getlistall")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _appGroupService.GetAll();
                var modelVm = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }


        [Route("detail/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " is required.");
            }
            ApplicationGroup appGroup = _appGroupService.GetDetail(id);
            var appGroupViewModel = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(appGroup);
            if (appGroup == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }
            var listRole = _appRoleService.GetListRoleByGroupId(appGroupViewModel.Id);
            appGroupViewModel.Roles = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRole);
            return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "ManageUser")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppGroup = new ApplicationGroup();
                newAppGroup.Name = appGroupViewModel.Name;
                newAppGroup.Description = appGroupViewModel.Description;
                try
                {
                    var appGroup = _appGroupService.Add(newAppGroup);
                    _appGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.Id);
                    _appRoleService.Save();


                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);


                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }

            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "ManageUser")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var appGroup = _appGroupService.GetDetail(appGroupViewModel.Id);
                try
                {
                    appGroup.UpdateApplicationGroup(appGroupViewModel);
                    _appGroupService.Update(appGroup);
                    //_appGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.Id);
                    _appRoleService.Save();

                    //add role to user
                    var listRole = _appRoleService.GetListRoleByGroupId(appGroup.Id);
                    var listUserInGroup = _appGroupService.GetListUserByGroupId(appGroup.Id);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToArray();
                        foreach (var roleName in listRoleName)
                        {
                            await _userManager.RemoveFromRoleAsync(user.Id, roleName);
                            await _userManager.AddToRoleAsync(user.Id, roleName);
                        }
                    }
                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }

            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "ManageUser")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = _appGroupService.Delete(id);
            _appGroupService.Save();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Authorize(Roles = "ManageUser")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _appGroupService.Delete(item);
                    }

                    _appGroupService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}
