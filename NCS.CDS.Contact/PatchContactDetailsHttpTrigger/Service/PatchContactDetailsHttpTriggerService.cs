﻿using System;
using System.Net;
using System.Threading.Tasks;
using NCS.DSS.Contact.Cosmos.Provider;
using NCS.DSS.Contact.Models;
using NCS.DSS.Contact.ServiceBus;

namespace NCS.DSS.Contact.PatchContactDetailsHttpTrigger.Service
{
    public class PatchContactDetailsHttpTriggerService : IPatchContactDetailsHttpTriggerService
    {
        public async Task<ContactDetails> UpdateAsync(ContactDetails contactdetails, ContactDetailsPatch contactdetailsPatch)
        {
            if (contactdetails == null)
                return null;

            contactdetailsPatch.SetDefaultValues();
            contactdetails.Patch(contactdetailsPatch);

            var documentDbProvider = new DocumentDBProvider();
            var response = await documentDbProvider.UpdateContactDetailsAsync(contactdetails);

            var responseStatusCode = response.StatusCode;

            return responseStatusCode == HttpStatusCode.OK ? contactdetails : null;
        }

        public async Task<ContactDetails> GetContactDetailsForCustomerAsync(Guid customerId, Guid contactId)
        {
            var documentDbProvider = new DocumentDBProvider();
            var contactdetails = await documentDbProvider.GetContactDetailForCustomerAsync(customerId, contactId);

            return contactdetails;
        }

        public async Task SendToServiceBusQueueAsync(ContactDetails contactdetails, Guid customerId, string reqUrl)
        {
            await ServiceBusClient.SendPatchMessageAsync(contactdetails, customerId, reqUrl);
        }
    }
}
