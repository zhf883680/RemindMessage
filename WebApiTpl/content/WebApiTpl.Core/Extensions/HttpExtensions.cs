
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTpl.Core.Extensions
{
    public class HttpExtensions
    {
        /// <summary>
        ///     get请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<string> HttpGet(HttpClient client, string url, int timeout = 100)
        {
            try
            {
                //HttpClientHandler clientHandler = new HttpClientHandler();
                ////忽略证书
                //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                client.Timeout = TimeSpan.FromSeconds(timeout);
                var msg = await client.GetAsync(url);
                if (msg.StatusCode != HttpStatusCode.OK) return "error";
                return await msg.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"url请求失败{url}");
                Console.WriteLine(ex.ToString());
            }

            return "error";
        }

        /// <summary>
        ///     post请求
        /// </summary>
        /// <param name="client"></param>
        /// <param name="url"></param>
        /// <param name="httpContent"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> HttpPost(HttpClient client, string url, HttpContent httpContent,
            string token = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var msg = await client.PostAsync(url, httpContent);
                if (msg.StatusCode != HttpStatusCode.OK) return "error";
                return await msg.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"url请求失败{url}");
                Console.WriteLine(ex.ToString());
            }

            return "error";
        }
        /// <summary>
        /// tcp连接测试
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="defaultPort"></param>
        /// <returns></returns>
        public static bool TcpConnect(string ip, string defaultPort)
        {
            var ipAddress = System.Net.IPAddress.Parse(ip);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ipAddress, Convert.ToInt32(defaultPort));
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        #region 获取mac地址

        [DllImport("Iphlpapi.dll")]
        static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);

        [DllImport("Ws2_32.dll")]
        static extern Int32 inet_addr(string ipaddr);

        /// <summary>
        /// 获取mac地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetMac(string ip)
        {
            if (ip == "::1" || ip == "127.0.0.1")
            {
                return "";
            }
            else if (ip.StartsWith("::ffff:"))
            {
                ip = ip.Substring(7);
            }

            StringBuilder macAddress = new StringBuilder();
            try
            {
                Int32 remote = inet_addr(ip);
                Int64 macInfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macInfo, ref length);
                string temp = Convert.ToString(macInfo, 16).PadLeft(12, '0').ToUpper();
                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5)
                    {
                        macAddress.Append(temp.Substring(x - 2, 2));
                    }
                    else
                    {
                        macAddress.Append(temp.Substring(x - 2, 2) + "-");
                    }

                    x -= 2;
                }
            }
            catch
            {
                return "Error";
            }

            return macAddress.ToString();
        }

        #endregion
    }
}