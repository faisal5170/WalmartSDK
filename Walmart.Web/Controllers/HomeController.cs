using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Walmart.Sdk.Marketplace;
using Walmart.Sdk.Marketplace.V2.Api;
using Walmart.Sdk.Marketplace.V2.Payload.Feed;

namespace Walmart.Web.Controllers
{
    public class HomeController : Controller
    {
        private FeedEndpoint feedApi;
        private ItemEndpoint itemApi;
        protected ClientConfig config;
        protected ApiClient client;

        public HomeController()
        {
            //client = InitApiClient();
            InitApiClient();
            itemApi = new ItemEndpoint(client);
            feedApi = new FeedEndpoint(client);
        }

        // GET: Home
        //public ActionResult Index()
        //{
        //    var str = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        //<MPItemFeed xmlns=""http://walmart.com/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://walmart.com/MPItem.xsd"">
        //<MPItemFeedHeader>
        //<version>3.1</version>
        //<requestId>333pecEl55nics</requestId>
        //<requestBatchId>333ecElect55cs38</requestBatchId>
        //</MPItemFeedHeader>   
        //<MPItem>
        //<processMode>CREATE</processMode>
        //<sku>SKUDhirenTest</sku>
        //<Product>
        //<productName>Title Description 22 </productName>
        //<longDescription>Title Description 22</longDescription>
        //<shelfDescription>Title Description 22</shelfDescription>
        //<shortDescription>Title Description 22</shortDescription>
        //<mainImage>
        //<mainImageUrl>http://images.antonline.com/img-main/500/037229400328.jpg</mainImageUrl>
        //</mainImage>
        //<productIdentifiers>
        //<productIdentifier>
        //<productIdType>UPC</productIdType>
        //<productId></productId>
        //</productIdentifier>
        //</productIdentifiers>
        //<productTaxCode>2038710</productTaxCode>
        //<additionalProductAttributes>
        //<additionalProductAttribute>
        //<productAttributeName>product_id_override</productAttributeName>
        //<productAttributeValue>true</productAttributeValue>
        //</additionalProductAttribute>
        //</additionalProductAttributes>
        //<category>
        //<Baby>
        //<brand></brand>
        //<BabyClothing>
        //</BabyClothing>
        //</Baby>
        //</category>
        //</Product>
        //<price>
        //<currency>USD</currency>
        //<amount>122.33</amount>
        //</price>
        //<shippingWeight>
        //<value>0</value>
        //<unit>LB</unit>
        //</shippingWeight>
        //</MPItem>
        //</MPItemFeed>";
        //    MemoryStream stream = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(stream);
        //    writer.Write(str);
        //    writer.Flush();
        //    stream.Position = 0;
        //    using (Stream requestStream = new Stream())
        //    {
        //        stream.CopyTo(requestStream);
        //        var response = BulkItemsUpdate(requestStream);
        //    }
        //    return View();
        //}

        public ActionResult Index()
        {
            //Listing
            //var latestSku = itemApi.GetAllItems().Result;
            //GetItem
            //var onesku = itemApi.GetItem("MRTSHIRT101").Result;
            string str = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<MPItemFeed xmlns=""http://walmart.com/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://walmart.com/ MPItem.xsd "">
  <MPItemFeedHeader>
    <version>2.1</version>
    <requestId>qqq</requestId>
    <requestBatchId>qqq1</requestBatchId>
  </MPItemFeedHeader>
  <MPItem>
    <sku>qqq</sku>
    <Product>
      <productName>QQQ 1-Foot S-Video Male to 2 S-Video Female Y Cable (CSV2F)</productName>
      <longDescription><![CDATA[<div class=""productDescriptionWrapper""> QVS Premium S-Video Mini4 Male to Two Female Splitter Cable CSV2F A/V Device Cables <div class=""emptyClear"">
      </div>
      </div>]]></longDescription>
      <shelfDescription><![CDATA[QVS 1-Foot S-Video Male to 2 S-Video Female Y Cable (CSV2F)]]></shelfDescription>
      <shortDescription>QQQ 1-Foot S-Video Male to 2 S-Video Female Y Cable (CSV2F)</shortDescription>
      <mainImage>
        <mainImageUrl>http://images.antonline.com/img-main/500/037229400328.jpg</mainImageUrl>
      </mainImage>
      <productIdentifiers>
        <productIdentifier>

        </productIdentifier>
      </productIdentifiers>
      <productTaxCode>2038710</productTaxCode>
      <Electronics>
        <brand>QQQ</brand>
        <ElectronicsCables>
        </ElectronicsCables>
      </Electronics>
    </Product>
    <price>
      <currency>USD</currency>
      <amount>12.34</amount>
    </price>
    <shippingWeight>
      <value>1.234</value>
      <unit>LB</unit>
    </shippingWeight>
  </MPItem>
</MPItemFeed>";

            var res = feedApi.UploadFeed(GenerateStreamFromString(str), Sdk.Marketplace.V2.Payload.FeedType.item);
            return View();
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public void InitApiClient()
        {
            config = new Sdk.Marketplace.ClientConfig(
                "consumerid",
                "privatekey"
            );
            config.BaseUrl = "https://marketplace.walmartapis.com";
            config.ChannelType = "ctype";
            config.ServiceName = "Walmart Marketplace";
            client = new ApiClient(config);
        }

        //public async Task<FeedAcknowledgement> BulkItemsUpdate(System.IO.Stream stream)
        //{
        //    return await feedApi.UploadFeed(stream, Sdk.Marketplace.V2.Payload.FeedType.item);
        //}

    }
}