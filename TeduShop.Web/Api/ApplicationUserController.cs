using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TeduShop.Common.Exceptions;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;


namespace TeduShop.Web.Api
{
    [RoutePrefix("api/applicationUser")]
    [Authorize]
    public class ApplicationUserController : ApiControllerBase
    {
        private ApplicationUserManager _appLicationUserManager;
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _applicationRoleService;
        public ApplicationUserController(IApplicationRoleService applicationRoleService,IApplicationGroupService applicationGroupService, IErrorService errorService, ApplicationUserManager applicationUserManager) : base(errorService)
        {
            this._applicationRoleService = applicationRoleService;
            this._applicationGroupService = applicationGroupService;
            this._appLicationUserManager = applicationUserManager;
        }

        [Route("getlistpaging")]

        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _appLicationUserManager.Users;
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                IEnumerable<ApplicationUserViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(query);

                PaginationSet<ApplicationUserViewModel> pagedSet = new PaginationSet<ApplicationUserViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [HttpPost]
        [Route("create")]

        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser();
                newAppUser.UpdateApplicationUser(applicationUserViewModel);
                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = await _appLicationUserManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();

                        foreach (var item in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = item.ID,
                                UserId = newAppUser.Id
                            });
                            //add role to group; 
                            var listRole = _applicationRoleService.GetListRoleByGroupId(item.ID);
                            foreach (var role in listRole)
                            {
                               await _appLicationUserManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                               await _appLicationUserManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        _applicationGroupService.AddUserToGroups(listAppUserGroup, newAppUser.Id);
                        _applicationGroupService.Save();

                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    else
                    {
                        return request.CreateResponse(HttpStatusCode.OK, result.Errors);
                    }

                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _appLicationUserManager.FindByIdAsync(applicationUserViewModel.Id);
                try
                {
                    appUser.UpdateApplicationUser(applicationUserViewModel);
                    var result = await _appLicationUserManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<ApplicationUserGroup>();

                        foreach (var item in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new ApplicationUserGroup()
                            {
                                GroupId = item.ID,
                                UserId = applicationUserViewModel.Id
                            });
                            //add role to group; 
                            var listRole = _applicationRoleService.GetListRoleByGroupId(item.ID);
                            foreach (var role in listRole)
                            {
                                await _appLicationUserManager.RemoveFromRoleAsync(applicationUserViewModel.Id, role.Name);
                                await _appLicationUserManager.AddToRoleAsync(applicationUserViewModel.Id, role.Name);
                            }
                        }
                        _applicationGroupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
                        _applicationGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }

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
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        {
            var appUser = await _appLicationUserManager.FindByIdAsync(id);
            var result = await _appLicationUserManager.DeleteAsync(appUser);
            if (result.Succeeded)
            {
                return request.CreateResponse(HttpStatusCode.OK, appUser.UserName);

            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
            }
        }

        [Route("getbyid/{id}")]

        [HttpGet]
        public async Task<HttpResponseMessage> GetById(HttpRequestMessage request, string id)
        {
            if (id == null)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, "Id không thể null");
            }
            else
            {
                var userDetail = await _appLicationUserManager.FindByIdAsync(id);
                var mapper = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(userDetail);
                var groups = _applicationGroupService.GetListGroupByUserId(id);
                mapper.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(groups);
                if (userDetail != null)
                {
                    return request.CreateResponse(HttpStatusCode.OK, mapper);
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.OK, "not fount Data with this Id. Maybe wrong or missing Id in Database");
                }

            }
        }


    }

}
