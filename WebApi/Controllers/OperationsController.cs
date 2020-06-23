using Business;
using Common.Models;
using Common.Support;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using WebApi.OutputCache.V2;

namespace WebApi.Controllers
{
    [CacheOutput(ServerTimeSpan = 120)]
    public class OperationsController : ApiController
    {
        private OperationBusiness _operationBusiness;

        public OperationsController()
        {
            _operationBusiness = new OperationBusiness();
        }

        //GET: v1/operacoes
        public IHttpActionResult Get()
        {
            return Ok(_operationBusiness.GetAll());
        }

        // GET: api/operacoes?groupBy=1
        public IHttpActionResult GetByGroup(GroupByEnum groupBy)
        {
            return Ok(_operationBusiness.GetByGroup(groupBy));
        }

        //GET: api/operations/export?groupBy=1&fileType=csv
        [Route("api/operations/export")]
        public HttpResponseMessage GetFileExport(GroupByEnum groupBy, string fileType)
        {
            if (string.IsNullOrEmpty(fileType))
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent($"Requisição inválida.")};

            byte[] ByteArray = null;
            var downloadFileType = new DownloadFileInfo(fileType);

            if (downloadFileType.FileType == DownloadFileTypesEnum.Xls)
                ByteArray = _operationBusiness.ExportXls(_operationBusiness.GetByGroup(groupBy).ToList(), groupBy);

            if (downloadFileType.FileType == DownloadFileTypesEnum.Csv)
                ByteArray = _operationBusiness.ExportCSV(_operationBusiness.GetByGroup(groupBy).ToList(), groupBy);

            return ByteArray.ExcelResult("Operações", downloadFileType);
        }
    }
}
