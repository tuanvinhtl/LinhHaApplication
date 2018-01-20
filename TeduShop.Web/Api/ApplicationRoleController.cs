using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/applicationRole")]
    [Authorize]
    public class ApplicationRoleController : ApiControllerBase
    {
        IApplicationRoleService _applicationRoleService;
        public ApplicationRoleController(IErrorService errorService, IApplicationRoleService applicationRoleService) : base(errorService)
        {
            this._applicationRoleService = applicationRoleService;
        }


        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, int totalRow, string fillter)
        {
            return CreateHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;
                 totalRow = 0;
                 var model = _applicationRoleService.GetAll(page, pageSize, out totalRow, fillter);
                 var mapper = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);
                 var paginationSet = new PaginationSet<ApplicationRoleViewModel>
                 {
                     TotalCount = totalRow,
                     Page = page,
                     TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                     Items = mapper
                 };
                 response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                 return response;
             });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _applicationRoleService.GetAll();
                var mapper = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ApplicationRole appRole = _applicationRoleService.GetDetail(Id);
                var mapper = Mapper.Map<ApplicationRole, ApplicationRoleViewModel>(appRole);
                if (string.IsNullOrEmpty(Id))
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "No Id for this Method");
                }
                if (appRole == null)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "No data response");
                }
                response = request.CreateResponse(HttpStatusCode.OK, mapper);

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, string Id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _applicationRoleService.Delete(Id);
                _applicationRoleService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, "Xóa thành công");
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationRoleViewModel appRoleViewModel)
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
                    ApplicationRole appRole = new ApplicationRole();
                    appRole.UpdateApplicationRole(appRoleViewModel, "create");
                    var result = _applicationRoleService.Add(appRole);
                    _applicationRoleService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, result);
                }
                return response;
            });
        }
        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationRoleViewModel appRoleViewModel)
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
                    ApplicationRole appRole = new ApplicationRole();
                    appRole.UpdateApplicationRole(appRoleViewModel, "update");
                    _applicationRoleService.Update(appRole);
                    _applicationRoleService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, appRoleViewModel);
                }
                return response;
            });
        }


    }
}
