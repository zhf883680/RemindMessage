

using System.Net.Http;
using WebApiTpl.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiTpl.Controllers 
{
    public class BaseController: ControllerBase
    {
        public HttpClient _client { get; private set; }
        private protected readonly IWebHostEnvironment _hostingEnvironment;
        private protected readonly IHttpContextAccessor _httpContextAccessor;
        private protected readonly IConfiguration _configuration;
        public BaseController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            //忽略证书
            HttpClientHandler clientHandler = new ();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _client = new HttpClient(clientHandler);
        }
       
        /// <summary>
        /// 返回json格式数据
        /// </summary>
        /// <param name="errcode">0:成功 非0:失败</param>
        /// <param name="errmsg">错误信息</param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IActionResult ReturnMsg(int errcode, object errmsg)
        {
            return Ok(new ResponseData{ Errcode=errcode, Errmsg=errmsg });
        }
        /// <summary>
        /// 返回json格式数据(客户端)
        /// </summary>
        /// <param name="ErrCode"></param>
        /// <param name="ErrMsg"></param>
        /// <param name="result"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IActionResult ReturnMsg(int ErrCode, string ErrMsg, object result, string Action = "")
        {
            return new JsonResult(new
            {
                ErrCode,
                ErrMsg,
                Response = SecurityExtensions.AESEncryption(result.ToString() == "[]"
                    ? "[]"
                    : JsonExtensions.ModelToJson(result)),
                Version = 1,
                Action
            }, new System.Text.Json.JsonSerializerOptions() { WriteIndented = false });
        }
        /// <summary>
        /// 返回列表所需要的数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="count"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public IActionResult ReturnTableData(int code, string msg, int count, object data)
        {
            return Ok(new ResponseTableData
            {
                Errcode=code,
                Errmsg=msg,
                Total=count,
                Data=JsonExtensions.ModelToJson(data)
            });
               
        }

        public class BaseRequestTable
        {
            public int page { get; set; }
            public int limit { get; set; }
        }
        /// <summary>
        /// 返回列表数据
        /// </summary>
        public class ResponseTableData:ResponseData
        {
            public int Total { get; set; }
            public object Data { get; set; }
        }
        /// <summary>
        /// 返回数据
        /// </summary>
        public class ResponseData
        {
            public int Errcode { get; set; }
            public object Errmsg { get; set; }
        }
        /// <summary>
        /// 401错误码返回值
        /// </summary>
        public class BaseError401Response
        {
            public string type { get; set; }
            public string title { get; set; }
            public int status { get; set; }
            public string traceId { get; set; }

            /// <summary>
            /// 根据不同的请求参数,显示不同的错误
            /// </summary>
            public object errors { get; set; }
        }
    }
}