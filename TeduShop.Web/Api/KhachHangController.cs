using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Model.Models;
using AutoMapper;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/khachHang")]

    public class KhachHangController : ApiControllerBase
    {
        IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService, IErrorService errorService) : base(errorService)
        {
            this._khachHangService = khachHangService;
        }
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, KhachHangViewModel khachhang)
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
                    bool result = CheckKhachHang(khachhang.Name);
                    if (result)
                    {
                        KhachHang NewKh = new KhachHang();
                        NewKh.UpdateKhachHang(khachhang);
                        var model = _khachHangService.Add(NewKh);
                        var mapper = Mapper.Map<KhachHang, KhachHangViewModel>(model);
                        _khachHangService.SaveChange();
                        response = request.CreateResponse(HttpStatusCode.OK, mapper);
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, "Tên không được trùng");
                    }

                }

                return response;
            });
        }
        [Route("Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, KhachHangViewModel khachhang)
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
                    KhachHang NewKh = new KhachHang();
                    NewKh.UpdateKhachHang(khachhang);
                    _khachHangService.Edit(NewKh);
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
                    _khachHangService.Delete(id);
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

        [Route("GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _khachHangService.GetAll();
                var mapper = Mapper.Map<IEnumerable<KhachHang>, IEnumerable<KhachHangViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, mapper);

                return response;
            });
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyWord, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _khachHangService.GetListPaging(page, pageSize, out totalRow, keyWord);
                IEnumerable<KhachHangViewModel> modelVm = Mapper.Map<IEnumerable<KhachHang>, IEnumerable<KhachHangViewModel>>(model);

                PaginationSet<KhachHangViewModel> pagedSet = new PaginationSet<KhachHangViewModel>()
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
                var model = _khachHangService.GetDetail(id);
                if (model == null)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, "no data");
                }
                else
                {
                    var mapper = Mapper.Map<KhachHang, KhachHangViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.OK, mapper);
                }

                return response;
            });
        }

        private bool CheckKhachHang(string tenkhachhang)
        {
            bool chcks = true;
            var khachhang = _khachHangService.GetAll();
            foreach (var item in khachhang)
            {
                if (item.Name == tenkhachhang)
                {
                    chcks = false;
                }
            }
            return chcks;
        }

    }
}
