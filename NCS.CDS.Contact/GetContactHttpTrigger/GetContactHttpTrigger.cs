﻿using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

namespace NCS.CDS.Contact.GetContactHttpTrigger
{
    public static class GetContactHttpTrigger
    {
        [FunctionName("GetContact")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customers/contacts/")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function GetContact processed a request.");

            var service = new GetContactHttpTriggerService();
            var values = await service.GetContacts();
            
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values),
                    System.Text.Encoding.UTF8, "application/json")
            };
        }

    }
}
