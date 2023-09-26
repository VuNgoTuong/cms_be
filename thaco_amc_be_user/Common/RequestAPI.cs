using Common;
using Common.Commons;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Config;

namespace UserManagement.Common
{
    public class RequestAPI
    {
        //public HttpClient client;
        //private readonly static ConfigManager _configManager = new ConfigManager();
        //private string hostFabio = ConfigManager.StaticGet(Constants.CONF_HOST_FABIO_SERVICE);

        //public RequestAPI() { }
        //private void DefaultSetting()
        //{
        //    var handler = new HttpClientHandler { UseDefaultCredentials = true };
        //    client = new HttpClient(handler);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.
        //        Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}
        //public RequestAPI(string confBaseUrl, string token = "")
        //{
        //    DefaultSetting();
        //    client.BaseAddress = new Uri(ConfigManager.StaticGet(confBaseUrl));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}
        //// rest api by consult
        //public RequestAPI(ResponseService<string> confBaseUrl, string token = "", string preDomainApi = "/api/")
        //{
        //    DefaultSetting();
        //    client.BaseAddress = new Uri(confBaseUrl.data + preDomainApi);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        //// rest api by fabio
        //public RequestAPI ToFabio(string sourceFabio, string token = "", string preDomainApi = "/api/")
        //{
        //    DefaultSetting();
        //    client.BaseAddress = new Uri(hostFabio + sourceFabio + preDomainApi);
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    return this;
        //}
        //public class MiddlewareHandler : DelegatingHandler
        //{
        //    public MiddlewareHandler(HttpMessageHandler innerHandler)
        //    : base(innerHandler)
        //    {
        //    }

        //    protected async override Task<HttpResponseMessage> SendAsync(
        //        HttpRequestMessage request,
        //        CancellationToken cancellationToken)
        //    {
        //        //var requestDate = request.Headers.Date;
        //        // do something with the date ...

        //        // handle respose
        //        var response = await base.SendAsync(request, cancellationToken);
        //        if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            string mess = string.Format("{0} {1} {2}", response.ReasonPhrase.ToString(), ((int)response.StatusCode).ToString(), response.RequestMessage.RequestUri);
        //            await CommonFunc.LogErrorToKafka(mess);
        //        }

        //        return response;
        //    }
        //}
    }
}
