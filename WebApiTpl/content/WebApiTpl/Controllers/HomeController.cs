
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApiTpl.Controllers
{
    [ApiController]
    public class HomeController:BaseController
    {
        public HomeController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
        ) : base(hostingEnvironment, configuration, httpContextAccessor)
        {
        }

        public class LoginRequest
        {
            [Required(ErrorMessage = "请输入账户")]
            [MaxLength(32,ErrorMessage = "账户长度超出限制")]
            public string Account { get; set; }
            [Required(ErrorMessage = "请输入密码")]
            [MaxLength(32,ErrorMessage = "密码长度超出限制")]
            public string Password { get; set; }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">请求成功</response>
        /// <response code="400">参数错误</response>
        /// <response code="403">请求次数过多</response>
        [Route("home/[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseData), 200)]
        [ProducesResponseType(typeof(BaseError401Response), 400)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            Console.WriteLine(request.Account);
            Console.WriteLine(request.Password);
            return ReturnMsg(0, "成功");
        }
    }
}