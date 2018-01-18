using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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


        [Route("exportExcel")]
        [HttpGet]
        public HttpResponseMessage ExportExcel(HttpRequestMessage request)
        {
            var folderReport = "/Reports";
            string document = OutExcelAll();
            return request.CreateErrorResponse(HttpStatusCode.OK, folderReport + "/" + document);
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

        private string OutExcel(int productId)
        {
            var folderReport = "/Reports";
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // template File
            string templateDocument = HttpContext.Current.Server.MapPath("~/Reports/TemplateForReport/ProductReport.xlsx");
            string documentName = string.Format("Product-{0}-{1}.xlsx", productId, DateTime.Now.ToString("ddmmyyyy"));
            string fullPath = Path.Combine(filePath, documentName);
            //result Output
            MemoryStream output = new MemoryStream();

            //read template
            FileStream templateDocumentStream = File.OpenRead(templateDocument);
            ExcelPackage package = new ExcelPackage(templateDocumentStream);
            ExcelWorksheet sheet = package.Workbook.Worksheets["ProductReportId"];
            //var product = _chitietKhachHangService.GetById(productId);
            //sheet.Cells[6, 1].Value = productId;
            //sheet.Cells[6, 2].Value = product.Name;
            //sheet.Cells[6, 3].Value = product.Alias;
            //sheet.Cells[6, 4].Value = product.Price;
            //sheet.Cells[6, 5].Value = product.CategoryID;
            //sheet.Cells[6, 6].Value = product.Status;
            package.SaveAs(new FileInfo(fullPath));
            return documentName;
        }

        private string OutExcelAll()
        {
            var folderReport = "/Reports";
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // template File
            string templateDocument = HttpContext.Current.Server.MapPath("~/Reports/TemplateForReport/ProductReport.xlsx");
            string documentName = string.Format("Product-{0}.xlsx", DateTime.Now.ToString("ddmmyyyyss"));
            string fullPath = Path.Combine(filePath, documentName);
            //result Output
            MemoryStream output = new MemoryStream();

            //read template
            FileStream templateDocumentStream = File.OpenRead(templateDocument);
            ExcelPackage package = new ExcelPackage(templateDocumentStream);
            ExcelWorksheet sheet = package.Workbook.Worksheets["ProductReportId"];
            var khachhang = _chitietKhachHangService.GetAll();
            var sahuynh = _chitietKhachHangService.GetBySaHuynh();
            var thanhduc = _chitietKhachHangService.GetByThanhDuc();
            var con = _chitietKhachHangService.GetByCon();
            var tandiem = _chitietKhachHangService.GetTanDiem();
            var tanloc = _chitietKhachHangService.GetTanLoc();
            var lavan = _chitietKhachHangService.GetLaVan();

            decimal lavantl = 0;
            foreach (var item in lavan)
            {
                lavantl += item.CTNoLai;
            }


            decimal sahuynhtl = 0;
            foreach (var item in sahuynh)
            {
                sahuynhtl += item.CTNoLai;
            }

            decimal thanhductl = 0;
            foreach (var item in thanhduc)
            {
                thanhductl += item.CTNoLai;
            }

            decimal tandiemtl = 0;
            foreach (var item in tandiem)
            {
                tandiemtl += item.CTNoLai;
            }

            decimal tanloctl = 0;
            foreach (var item in tanloc)
            {
                tanloctl += item.CTNoLai;
            }

            decimal contl = 0;
            foreach (var item in con)
            {
                contl += item.CTNoLai;
            }

            sheet.Cells[3, 6].Value = sahuynhtl;
            sheet.Cells[4, 6].Value = thanhductl;
            sheet.Cells[5, 6].Value = tandiemtl;
            sheet.Cells[6, 6].Value = tanloctl;
            sheet.Cells[7, 6].Value = contl;
            sheet.Cells[8, 6].Value = lavantl;



            int i = 0;
            decimal totalCountMoney = 0;
            foreach (var item in khachhang)
            {

                totalCountMoney += item.CTNoLai;
                sheet.Cells[12 + i, 1].Value = item.IdKhachHang;
                sheet.Cells[12 + i, 2].Value = item.Name;
                sheet.Cells[12 + i, 3].Value = item.Address;
                sheet.Cells[12 + i, 4].Value = item.ChiPhiChuaBenh;
                sheet.Cells[12 + i, 5].Value = item.TraTruoc;
                sheet.Cells[12 + i, 6].Value = item.CTNoLai;
                i++;
            }
            sheet.Cells[10, 6].Value = totalCountMoney;
            sheet.Cells[10, 1].Value = DateTime.Now.ToString("dd/MM/yyyy");
            package.SaveAs(new FileInfo(fullPath));

            return documentName;
        }
    }
}
