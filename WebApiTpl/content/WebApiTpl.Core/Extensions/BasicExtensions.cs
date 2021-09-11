using System;

namespace WebApiTpl.Core.Extensions
{
    public class BasicExtensions
    {
        /// <summary>
        /// 获取程序运行路径
        /// </summary>
        /// <returns></returns>
        public static string GetBasicPath()
        {
            return $"{AppContext.BaseDirectory}/";
        }
    }
}