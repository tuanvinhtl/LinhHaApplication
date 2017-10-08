using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TeduShop.Web.Api.InterFaceForApiController
{
    public interface InterfaceForApiControllerBase<T>
    {

        HttpResponseMessage Create(HttpRequestMessage request, T entity);


        HttpResponseMessage Update(HttpRequestMessage request, T entity);


        HttpResponseMessage Delete(HttpRequestMessage request, int id);


        HttpResponseMessage GetAll(HttpRequestMessage request);


        HttpResponseMessage GetListPaging(HttpRequestMessage request,string keyWord, int page, int pageSize);



        HttpResponseMessage GetDetailById(HttpRequestMessage request, int id);

    }
}
