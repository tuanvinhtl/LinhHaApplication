using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/applicationGroup")]
    [Authorize]
    public class ApplicationGroupController : ApiControllerBase
    {
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _appRoleService;
        private ApplicationUserManager _appUsermanager;
        public ApplicationGroupController(ApplicationUserManager appUsermanager,IApplicationRoleService appRoleService, IApplicationGroupService applicationGroupService, IErrorService errorService) : base(errorService)
        {
            this._appUsermanager = appUsermanager;
            this._appRoleService = appRoleService;
            this._applicationGroupService = applicationGroupService;
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationGroupViewModel applivationGroupViewModel)
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
                    ApplicationGroup appGroup = new ApplicationGroup();
                    appGroup.UpdateApplicationGroup(applivationGroupViewModel);
                    var model = _applicationGroupService.Add(appGroup);
                    _applicationGroupService.Save();
                    var mapperModel = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(model);
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var item in applivationGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = model.ID,
                            RoleId = item.Id
                        });
                    }
                    var role = _appRoleService.AddRolesToGroup(listRoleGroup, mapperModel.ID);
                    _appRoleService.Save();
                    var listRole = _appRoleService.GetListRoleByGroupId(applivationGroupViewModel.ID);
                    var listUserInGroup = _applicationGroupService.GetListUserByGroupId(applivationGroupViewModel.ID);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToArray();
                        foreach (var roleName in listRoleName)
                        {
                             _appUsermanager.AddToRoleAsync(user.Id, roleName);
                        }
                    }
                    response = request.CreateResponse(HttpStatusCode.Created, mapperModel);
                }
                return response;
            });

        }

        [AllowAnonymous]
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _applicationGroupService.GetAll();
                var mapperModel = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, mapperModel);
                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _applicationGroupService.Delete(id);
                _applicationGroupService.Save();
                var mapper = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(model);
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [HttpPut]
        [Route("update")]
        public async Task< HttpResponseMessage> Update(HttpRequestMessage request, ApplicationGroupViewModel applicaionGroupViewModel)
        {
            bool statusUpdate = false;
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                statusUpdate = false;
            }
            else
            {
                ApplicationGroup appGroup = new ApplicationGroup();
                appGroup.UpdateApplicationGroup(applicaionGroupViewModel);
                _applicationGroupService.Update(appGroup);
                var listRoleGroup = new List<ApplicationRoleGroup>();
                foreach (var item in applicaionGroupViewModel.Roles)
                {
                    listRoleGroup.Add(new ApplicationRoleGroup()
                    {
                        GroupId = applicaionGroupViewModel.ID,
                        RoleId = item.Id
                    });
                }
                var role = _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                _applicationGroupService.Save();
                statusUpdate = true;
                var listRole = _appRoleService.GetListRoleByGroupId(applicaionGroupViewModel.ID);
                var listUserInGroup = _applicationGroupService.GetListUserByGroupId(applicaionGroupViewModel.ID);
                foreach (var user in listUserInGroup)
                {
                    var listRoleName = listRole.Select(x => x.Name).ToArray();
                    foreach (var roleName in listRoleName)
                    {
                        await _appUsermanager.RemoveFromRoleAsync(user.Id, roleName);
                        await _appUsermanager.AddToRoleAsync(user.Id, roleName);
                    }
                }
                response = request.CreateResponse(HttpStatusCode.Created, statusUpdate);
            }
            return response;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _applicationGroupService.GetDetail(id);
                var mapper = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(model);
                var listRole = _appRoleService.GetListRoleByGroupId(id);
                mapper.Roles = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRole);
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

    }
}
