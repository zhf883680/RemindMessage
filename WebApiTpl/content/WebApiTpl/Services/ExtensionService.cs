using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
//using MySql.Data.MySqlClient;

namespace MCK.Services
{
    public interface IExtensionService
    {
        
        /// <summary>
        ///     获取db时间
        /// </summary>
        /// <returns></returns>
        DateTime GetMySqlTime();
        
    }
    public class ExtensionService:IExtensionService
    {
        
        public DateTime GetMySqlTime()
        {
            try
            {
                //var mysqlhelper = new MySqlHelper("");
                //var nowTime = mysqlhelper
                //    .ExecuteScalar("select DATE_FORMAT(NOW(),'%Y-%m-%d %H:%i:%s');", CommandType.Text, null).ToString();
                //return Convert.ToDateTime(nowTime);

            }
            catch
            {
                return DateTime.Now;
            }
            return DateTime.Now;
        }
      
    }
}