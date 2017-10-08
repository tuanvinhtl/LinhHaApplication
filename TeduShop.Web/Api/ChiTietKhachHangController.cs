using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Common.CommonModel;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/chitietKhachHang")]
    [Authorize]
    public class ChiTietKhachHangController : ApiControllerBase
    {
        IChiTietKhachHangService _chitietKhachHangService;
        IKhachHangService _khachHangService;

        public ChiTietKhachHangController(IKhachHangService khachHangService, IChiTietKhachHangService chitietKhachHangService, IErrorService errorService) : base(errorService)
        {
            this._chitietKhachHangService = chitietKhachHangService;
            this._khachHangService = khachHangService;
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyWordName, string keyWordAddress, string fromDate, string toDate, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _chitietKhachHangService.GetAllPaginAndFindByDate(page, pageSize, out totalRow, keyWordName, keyWordAddress, fromDate, toDate);

                PaginationSet<CTKhachHangModelCommon> pagedSet = new PaginationSet<CTKhachHangModelCommon>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = model
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

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
                var model = _chitietKhachHangService.GetAll();

                response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }


        [Route("CreateAny")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ModelCommon ctkhachhang)
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
                    ctkhachhang.KhachHang.CreatedDate = DateTime.Now;
                    var khachhang = _khachHangService.Add(ctkhachhang.KhachHang);
                    _khachHangService.SaveChange();

                    if (ctkhachhang.ChiTiet.NgayChuaBenh == DateTime.MinValue || ctkhachhang.ChiTiet.NgayChuaBenh == null)
                    {
                        ctkhachhang.ChiTiet.NgayChuaBenh = DateTime.Now;
                    }
                    ctkhachhang.ChiTiet.IdKhachHang = khachhang.Id;
                    var chitiet = _chitietKhachHangService.Add(ctkhachhang.ChiTiet);
                    _khachHangService.SaveChange();
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
                    _chitietKhachHangService.Delete(id);
                    _khachHangService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK, id);
                }
                catch (Exception)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest);
                }

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
                var model = _chitietKhachHangService.GetById(id);
                if (model == null)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "no data");
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.OK, model);
                }

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, CTKhachHangChuaBenhViewModel khachhang)
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
                    CTKhachHangChuaBenh NewKh = new CTKhachHangChuaBenh();
                    NewKh.UpdateCTKhachHangChuaBenh(khachhang);
                    _chitietKhachHangService.Update(NewKh);
                    _khachHangService.SaveChange();
                    response = request.CreateResponse(HttpStatusCode.OK, khachhang);
                }

                return response;
            });
        }

        [Route("getTotalAmount")]
        [HttpGet]
        public HttpResponseMessage GetTotalAmount(HttpRequestMessage request, string keyWordName, string keyWordAddress, string fromDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _chitietKhachHangService.GetAllTotalAmount(keyWordName, keyWordAddress, fromDate, toDate);

                response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("getlistbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetListById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _chitietKhachHangService.GetMultilById(id);
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }


        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, CTKhachHangChuaBenhViewModel ctKhachHangChuaBenhVM)
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
                    CTKhachHangChuaBenh ct = new CTKhachHangChuaBenh();

                    ct.UpdateCTKhachHangChuaBenh(ctKhachHangChuaBenhVM);
                    _chitietKhachHangService.Add(ct);
                    _khachHangService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.OK, ctKhachHangChuaBenhVM);
                }

                return response;
            });
        }
    }
}
