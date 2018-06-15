﻿using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Web.Http.Description;

namespace NCS.DSS.Contact.PostContactByIdHttpTrigger
{
    public static class PostContactByIdHttpTrigger
    {
        [FunctionName("POST")]
        [ResponseType(typeof(Models.Contact))]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "Post", Route = "customers/{customerId}/contacts/{contactid}")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function PostContact processed a request.");

            //if (!Guid.TryParse(contactid, out var contactGuid))
            //{
            //    return new HttpResponseMessage(HttpStatusCode.BadRequest)
            //    {
            //        Content = new StringContent(JsonConvert.SerializeObject(contactid),
            //            System.Text.Encoding.UTF8, "application/json")
            //    };
            //}

            var values = "Successfully created new contact details with Id : " + Guid.NewGuid();

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(values),
                    System.Text.Encoding.UTF8, "application/json")
            };
        }

    }
}
