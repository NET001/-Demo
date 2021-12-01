using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Security.Cryptography;

namespace Http_CJL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }


    }
    /// <summary>
    /// Http帮助类
    /// </summary>
    public static class HttpHelp
    {
        private static Dictionary<string, HttpClient> dictHttpClient = new Dictionary<string, HttpClient>();
        /// <summary>
        /// 发送http请求
        /// </summary>
        /// <param name="httpHelpConfig"></param>
        /// <returns></returns>
        public static string SendHttp(HttpHelpConfig httpHelpConfig)
        {
            HttpClient httpClient = GetHttpClient(httpHelpConfig);
            HttpResponseMessage response = null;
            try
            {
                if (httpHelpConfig.Method.ToLower() == "get")
                {
                    response = httpClient.GetAsync(httpHelpConfig.Url).Result;
                }
                else if (httpHelpConfig.Method.ToLower() == "post")
                {
                    if (httpHelpConfig.PostData != null)
                    {
                        using (HttpContent httpContent = new StringContent(httpHelpConfig.PostData))
                        {
                            response = httpClient.PostAsync(httpHelpConfig.Url, httpContent).Result;
                        }
                    }
                    else
                    {
                        response = httpClient.PostAsync(httpHelpConfig.Url, null).Result;
                    }
                }
                string responseBody = response?.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                response.Dispose();
            }
            //获取一个httpclient带缓存
            HttpClient GetHttpClient(HttpHelpConfig httpHelpConfig)
            {
                string keystr = "";
                if (httpHelpConfig.Url.IndexOf("https://") != -1)
                {
                    keystr += "https;";
                }
                else
                {
                    keystr += "http;";
                }
                foreach (var item in httpHelpConfig.Headers.Keys.OrderBy(t => t))
                {
                    keystr += item + httpHelpConfig.Headers[item] + ";";
                }
                string md5Key = keystr.ToMd5();
                HttpClient result = null;
                dictHttpClient.TryGetValue(md5Key, out result);
                if (result == null)
                {
                    if (httpHelpConfig.Url.IndexOf("https://") != -1)
                    {
                        result = new HttpClient(new HttpClientHandler()
                        {
                            ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
                        });
                    }
                    else
                    {
                        result = new HttpClient();
                    }
                    foreach (var item in httpHelpConfig.Headers.Keys)
                    {
                        result.DefaultRequestHeaders.Add(item, httpHelpConfig.Headers[item]);
                    }
                    if (result != null)
                    {
                        dictHttpClient[md5Key] = result;
                    }
                }
                return result;
            }
        }
        public class HttpHelpConfig
        {
            public string Url { get; set; }
            public string PostData { get; set; } = null;
            public string Method { get; set; } = "get";
            public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>()
            {
                ["ContentType"] = "application/json"
            };
        }
    }

    /// <summary>
    /// 密码学帮助类
    /// </summary>
    public static class CryptologyHelp
    {
        /// <summary>
        /// 进行md5运算
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }
    }
}
