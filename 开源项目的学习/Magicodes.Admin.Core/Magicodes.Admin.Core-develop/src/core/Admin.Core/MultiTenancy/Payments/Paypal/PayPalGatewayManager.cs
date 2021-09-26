// ======================================================================
// 
//           Copyright (C) 2019-2020 湖南心莱信息科技有限公司
//           All rights reserved
// 
//           filename : PayPalGatewayManager.cs
//           description :
// 
//           created by 雪雁 at  2019-06-17 10:17
//           开发文档: docs.xin-lai.com
//           公众号教程：magiccodes
//           QQ群：85318032（编程交流）
//           Blog：http://www.cnblogs.com/codelove/
//           Home：http://xin-lai.com
// 
// ======================================================================

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetZeroCore.Net;
using Abp.Dependency;
using Abp.Extensions;
using Abp.UI;
using Magicodes.Admin.Core.Editions;
using Newtonsoft.Json;

namespace Magicodes.Admin.Core.MultiTenancy.Payments.Paypal
{
    public class PayPalGatewayManager : AdminServiceBase, IPaymentGatewayManager, ITransientDependency
    {
        private readonly PayPalConfiguration _configuration;

        public PayPalGatewayManager(PayPalConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<CreatePaymentResponse> CreatePaymentAsync(string description, decimal amount)
        {
            var url = _configuration.BaseUrl.EnsureEndsWith('/') + "payments/payment";
            var requestBody = PayPalCreatePaymentRequest.Create(description, amount);
            return await MakeRequest<PayPalCreatePaymentResponse>(url, requestBody);
        }

        public async Task<ExecutePaymentResponse> ExecutePaymentAsync(Dictionary<string, string> data)
        {
            var paymentId = data["PaymentId"];
            var payerId = data["PayerId"];

            var url = _configuration.BaseUrl.EnsureEndsWith('/') + "payments/payment/" + paymentId + "/execute";
            var requestBody = new PayPalExecutePaymentRequest(payerId);
            return await MakeRequest<PayPalExecutePaymentResponse>(url, requestBody);
        }

        public Task<Dictionary<string, string>> GetAdditionalPaymentData(SubscribableEdition edition)
        {
            var additionalData = new Dictionary<string, string>
            {
                {"Environment", _configuration.Environment},
                {"DemoUsername", _configuration.DemoUsername},
                {"DemoPassword", _configuration.DemoPassword}
            };

            return Task.FromResult(additionalData);
        }

        private async Task<string> GetAuthToken()
        {
            var url = _configuration.BaseUrl.EnsureEndsWith('/') + "oauth2/token";
            var authToken =
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(_configuration.ClientId + ":" + _configuration.ClientSecret));

            using (var client = new HttpClient())
            {
                var request = CreateRequest(url, "Basic", authToken, client);
                request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                    "application/x-www-form-urlencoded");
                return (await ReadResponse<PayPalAccessTokenResult>(url, client, request)).AccessToken;
            }
        }

        private async Task<T> MakeRequest<T>(string url, object requestObj)
        {
            var authToken = await GetAuthToken();

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var request = CreateRequest(url, "Bearer", authToken, client);
                request.Content = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8,
                    MimeTypeNames.ApplicationJson);
                return await ReadResponse<T>(url, client, request);
            }
        }

        private static HttpRequestMessage CreateRequest(string url, string authSchema, string authToken,
            HttpClient client)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue(authSchema, authToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypeNames.ApplicationJson));
            return request;
        }

        private async Task<T> ReadResponse<T>(string url, HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Logger.Error($"Could not complete Paypal payment (url: {url}). Error: {error}");
                throw new UserFriendlyException(L("PaymentCouldNotCompleted"));
            }

            var success = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(success);
        }
    }
}