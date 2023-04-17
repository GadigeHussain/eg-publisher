//----------------------------------------------------------------------------------
// Microsoft Azure EventGrid Team
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace EventGridPublisher
{
    // This captures the "Data" portion of an EventGridEvent on a custom topic
    class ItemReceivedEventData
    {
        [JsonProperty(PropertyName = "itemSku")]
        public string ItemSku { get; set; }

        [JsonProperty(PropertyName = "ItemName")]
        public string ItemName { get; set; }

        [JsonProperty(PropertyName = "ItemCategory")]
        public int ItemCategory { get; set; }

        [JsonProperty(PropertyName = "ItemCode")]
        public string ItemCode { get; set; }


        [JsonProperty(PropertyName = "CustomProp1")]
        public string CustomProp1 { get; set; }


        [JsonProperty(PropertyName = "CustomProp2")]
        public int CustomProp2 { get; set; }


        [JsonProperty(PropertyName = "CustomProp3")]
        public double CustomProp3 { get; set; }


        [JsonProperty(PropertyName = "CustomProp4")]
        public bool CustomProp4 { get; set; }


    }

    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Enter values for <topic-name> and <region>. You can find this topic endpoint value
            // in the "Overview" section in the "Event Grid Topics" blade in Azure Portal.
            string topicEndpoint = "https://test-topic-01.southindia-1.eventgrid.azure.net/api/events";

            //string topicEndpoint = "https://srs-eg-demo-01.eastus-1.eventgrid.azure.net/api/events";
            // TODO: Enter value for <topic-key>. You can find this in the "Access Keys" section in the
            // "Event Grid Topics" blade in Azure Portal.
            string topicKey = "6JirnaMH4i9h3fC+R1NvMdrK2WuvOu2naSrdAh4iAUw=";

            //string topicKey = "rM7BGztUD/XZf7B4fGZ96Tvc6vP5omguoDeLwum4u9c=";

            string topicHostname = new Uri(topicEndpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);


            client.PublishEventsAsync(topicHostname, GetEventsList()).GetAwaiter().GetResult();
            Console.Write("Published events to Event Grid topic.");
            Console.ReadLine();
        }

        static IList<EventGridEvent> GetEventsList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();

            for (int i = 0; i < 1; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "Items.ItemReceived",
                    Data = new ItemReceivedEventData()
                    {
                        CustomProp1 = "property-2",
                        CustomProp2 = 2,
                        CustomProp3 = 2.1,
                        CustomProp4 = false
                    },
                    EventTime = DateTime.Now,
                    Subject = "Event's Subject",
                    DataVersion = "1.0"
                }); ;
            }

            return eventsList;
        }
    }
}

