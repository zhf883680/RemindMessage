
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
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
    }
}