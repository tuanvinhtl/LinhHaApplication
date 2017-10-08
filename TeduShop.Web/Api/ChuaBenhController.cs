using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Api.InterFaceForApiController;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    
    public interface IChuaBenhController: InterfaceForApiControllerBase<ChuaBenhViewModel>
    {
        
    }
    [RoutePrefix("api/chuaBenh")]
    [Authorize]
    public class ChuaBenhController : ApiControllerBase,IChuaBenhController
    {
        IChuaBenhService _chuabenhService;
        public ChuaBenhController(IChuaBenhService chuabenhService,IErrorService errorService) : base(errorService)
        {
            this._chuabenhService = chuabenhService;
        }

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ChuaBenhViewModel khachhang)
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
                    ChuaBenh NewKh = new ChuaBenh();
                    NewKh.UpdateChuaBenh(khachhang);
                    var model = _chuabenhService.Create(NewKh);
                    var mapper = Mapper.Map<ChuaBenh, ChuaBenhViewModel>(model);
                    _chuabenhService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK, mapper);
                }

                return response;
            });
        }
        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ChuaBenhViewModel khachhang)
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
                    ChuaBenh NewKh = new ChuaBenh();
                    NewKh.UpdateChuaBenh(khachhang);
                    _chuabenhService.Update(NewKh);
                    _chuabenhService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK, khachhang);
                }

                return response;
            });
        }

        [Route("Delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                try
                {
                    _chuabenhService.Delete(id);
                    _chuabenhService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK, id);
                }
                catch (Exception)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest);
                }

                return response;
            });
        }

        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _chuabenhService.GetAll();
                var mapper = Mapper.Map<IEnumerable<ChuaBenh>, IEnumerable<ChuaBenhViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, mapper);
                return response;
            });
        }

        [Route("getlistpaging")]

        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyWord, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _chuabenhService.GetAllPaging(page, pageSize, out totalRow, keyWord);
                IEnumerable<ChuaBenhViewModel> modelVm = Mapper.Map<IEnumerable<ChuaBenh>, IEnumerable<ChuaBenhViewModel>>(model);

                PaginationSet<ChuaBenhViewModel> pagedSet = new PaginationSet<ChuaBenhViewModel>()
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

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetDetailById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _chuabenhService.GetDetail(id);
                if (model == null)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "no data");
                }
                else
                {
                    var mapper = Mapper.Map<ChuaBenh, ChuaBenhViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.OK, mapper);
                }

                return response;
            });
        }
    }
}
