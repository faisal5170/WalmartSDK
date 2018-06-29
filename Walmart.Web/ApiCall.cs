//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Web;

//namespace Walmart.Web
//{
//    public class ApiCall
//    {
//        public string ConsumerID = "";
//        public string PrivateKey = "";
//        public string APIKey = "";
//        public string apiUrl = "https://marketplace.walmartapis.com/v3/";
//        public string ChannelType = "";       

        

//        public ApiCall(string ConsumerID, string PrivateKey, string APIKey, string ChannelType)
//        {

//            this.ConsumerID = ConsumerID;
//            this.PrivateKey = PrivateKey;
//            this.APIKey = APIKey;
//            this.ChannelType = ChannelType;
//        }

//        public void Upload_Item()
//        {

//        }

//        public bool IsValidKey(string Key)
//        {
//            try
//            {
//                string apiKey = Key;
//                var search = new SearchRequest() { apiKey = apiKey };
//                search.query = "VOD";
//                var searchResponse = ApiCall.Send<SearchResponse>(search);
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public string RequestUploadItem(ListingWalmartViewModel model)
//        {
//            string strBaseUrl = string.Format("https://marketplace.walmartapis.com/v3/feeds?feedType=item");
//            string strRequestMethod = "POST";
//            WallmartSigner Signature = new WallmartSigner(this.ConsumerID, this.PrivateKey, strBaseUrl, strRequestMethod);
//            string strSignature = string.Empty;
//            HttpWebRequest httpWebRequest = CreateRequestObject(strBaseUrl, strRequestMethod);
//            //null;
//            HttpWebResponse httpWebResponse = null;
//            //WalmartOrderModel orderModel = new WalmartOrderModel();
//            StringBuilder strBuilder = new StringBuilder();
//            bool bReturnValue = false;
//            try
//            {
//                string strRequestBody =
//        @"--BOUNDARYCommercebit \r\n<?xml version=""1.0"" encoding=""UTF-8""?>
//        <MPItemFeed xmlns=""http://walmart.com/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://walmart.com/MPItem.xsd"">
//        <MPItemFeedHeader>
//        <version>3.1</version>
//        <requestId>333pecEl55nics</requestId>
//        <requestBatchId>333ecElect55cs38</requestBatchId>
//        </MPItemFeedHeader>   
//        <MPItem>
//        <processMode>CREATE</processMode>
//    <feedDate>" + DateTime.Now + @"</feedDate>
//    <sku>" + model.SKU + @"</sku>    
//    <MPProduct>
//      <SkuUpdate>No</SkuUpdate>
//      <msrp>" + model.Price + @"</msrp>
//      <productName>" + model.Title + @"</productName>
//      <additionalProductAttributes>
//        <additionalProductAttribute>
//          <productAttributeName>UPC</productAttributeName>
//          <productAttributeValue>" + model.UPC + @"</productAttributeValue>
//        </additionalProductAttribute>
//      </additionalProductAttributes>
//      <ProductIdUpdate>Yes</ProductIdUpdate>
//      <category>
//         <" + model.Category.Replace(" ", "").Trim() + @">
//        <brand>" + model.Brand + @"</brand>
//        <" + model.SubCategory.Replace(" ", "").Trim() + @">
//        <mainImageUrl>http://images.antonline.com/img-main/500/037229400328.jpg</mainImageUrl>
//        </" + model.SubCategory.Replace(" ", "").Trim() + @">
//        </" + model.Category.Replace(" ", "").Trim() + @">
//      </category>
//    </MPProduct>
//    <MPOffer>
//      <price>" + model.Price + @"</price>
//      <MinimumAdvertisedPrice>" + model.Price + @"</MinimumAdvertisedPrice>
//      <StartDate>" + DateTime.Now + @"</StartDate>
//      <EndDate>" + DateTime.Now.AddYears(5) + @"</EndDate>
//      <MustShipAlone>Yes</MustShipAlone>
//      <ShippingWeight>
//        <measure>" + model.Weight + @"</measure>
//        <unit>lb</unit>
//      </ShippingWeight>
//      <ProductTaxCode>2038345</ProductTaxCode>
//      <shipsInOriginalPackaging>Yes</shipsInOriginalPackaging>      
//      <ShippingOverrides>
//        <ShippingOverrideAction>REPLACE_ALL</ShippingOverrideAction>
//        <shippingOverride>
//          <ShippingOverrideIsShippingAllowed>Yes</ShippingOverrideIsShippingAllowed>
//          <ShippingOverrideShipMethod>ONE_DAY</ShippingOverrideShipMethod>
//          <ShippingOverrideShipRegion>APO_FPO</ShippingOverrideShipRegion>
//          <ShippingOverrideshipPrice>100.00</ShippingOverrideshipPrice>
//        </shippingOverride>
//      </ShippingOverrides>
//    </MPOffer>
//  </MPItem>
//        </MPItemFeed>--BOUNDARYCommercebit-- \r\n \r\n";
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write(strRequestBody);
//                writer.Flush();
//                stream.Position = 0;
//                httpWebRequest.ContentLength = stream.Length;
//                using (Stream requestStream = httpWebRequest.GetRequestStream())
//                {
//                    stream.CopyTo(requestStream);
//                    //requestStream.Write(stream, 0, stream.Length);
//                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
//                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
//                    {
//                        Stream streamResponse = httpWebResponse.GetResponseStream();
//                        var responseResult = XDocument.Load(streamResponse);
//                    }
//                }

//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                return exMessage;

//            }
//            return "Item Uploaded Successfully";
//        }

//        public Tuple<bool, string> RequestDeleteItem(string SKU)
//        {
//            Tuple<bool, string> ResponseString;
//            try
//            {
//                string reqUrl = string.Format("items/{0}", SKU);
//                apiUrl = apiUrl + reqUrl;
//                string method = "DELETE";
//                string responseStr = "";
//                HttpWebRequest req = DeleteRequestObject(apiUrl, method);
//                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    Stream responseStream = response.GetResponseStream();
//                    responseStr = new StreamReader(responseStream).ReadToEnd();
//                    ResponseString = new Tuple<bool, string>(true, responseStr);

//                }
//                else
//                    ResponseString = new Tuple<bool, string>(false, responseStr);
//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                ResponseString = new Tuple<bool, string>(false, exMessage);
//            }
//            return ResponseString;
//        }

//        public string GetOrders()
//        {
//            apiUrl = apiUrl + "orders?createdStartDate=2018-01-01";
//            string method = "GET";
//            string responseStr = "";
//            try
//            {
//                HttpWebRequest req = CreateRequestObject(apiUrl, method);
//                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
//                if (response.StatusCode == HttpStatusCode.OK)
//                {
//                    Stream responseStream = response.GetResponseStream();
//                    responseStr = new StreamReader(responseStream).ReadToEnd();
//                }
//                else
//                    responseStr = "error";
//                return responseStr;
//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                return exMessage;
//            }
//        }

//        public string GetOrder(string reqUrl)
//        {
//            string method = "GET";
//            string responseStr = "";
//            HttpWebRequest req = CreateRequestObject(apiUrl, method);
//            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                Stream responseStream = response.GetResponseStream();
//                responseStr = new StreamReader(responseStream).ReadToEnd();
//            }
//            else
//                responseStr = "error";
//            return responseStr;
//        }

//        public string SendAcknowledge(string reqUrl)
//        {

//            string method = "POST";
//            HttpWebRequest req = CreateRequestObject(reqUrl, method);
//            req.MediaType = "application/xml";
//            req.ContentLength = 0;

//            HttpWebResponse response;
//            response = (HttpWebResponse)req.GetResponse();
//            string responseStr = "";
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                Stream responseStream = response.GetResponseStream();
//                responseStr = new StreamReader(responseStream).ReadToEnd();

//            }
//            else
//            {
//                responseStr = "error";
//            }
//            return responseStr;

//        }

//        public string SendShipping(string reqUrl, string xml)
//        {

//            string method = "POST";
//            HttpWebRequest req = CreateRequestObject(reqUrl, method);
//            byte[] bytes = Encoding.UTF8.GetBytes(xml);
//            req.GetRequestStream().Write(bytes, 0, bytes.Length);
//            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
//            string responseStr = "";
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                Stream responseStream = response.GetResponseStream();
//                responseStr = new StreamReader(responseStream).ReadToEnd();

//            }
//            else
//                responseStr = "error";

//            return responseStr;


//        }

//        public HttpWebRequest CreateRequestObject(string requestUrl, string method)
//        {
//            WallmartSigner Signature = new WallmartSigner(ConsumerID, PrivateKey, requestUrl, method);
//            string timeStamp = WallmartSigner.GetTimestampInJavaMillis();
//            string sigStr = Signature.GetWallmartSignature(timeStamp.ToString());
//            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
//            req.ContentType = "multipart/form-data; boundary=BOUNDARYCommercebit";
//            req.Accept = "application/xml";
//            req.Method = method;
//            req.Headers.Add("WM_SVC.NAME", "Walmart Marketplace");
//            req.Headers.Add("WM_SEC.AUTH_SIGNATURE", sigStr);
//            req.Headers.Add("WM_CONSUMER.ID", ConsumerID);
//            req.Headers.Add("WM_SEC.TIMESTAMP", timeStamp.ToString());
//            req.Headers.Add("WM_QOS.CORRELATION_ID", Guid.NewGuid().ToString());
//            req.Headers.Add("WM_CONSUMER.CHANNEL.TYPE", ChannelType);
//            return req;
//        }

//        public HttpWebRequest DeleteRequestObject(string requestUrl, string method)
//        {
//            WallmartSigner Signature = new WallmartSigner(ConsumerID, PrivateKey, requestUrl, method);
//            string timeStamp = WallmartSigner.GetTimestampInJavaMillis();
//            string sigStr = Signature.GetWallmartSignature(timeStamp.ToString());
//            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
//            req.Accept = "application/xml";
//            req.Method = method;
//            req.Headers.Add("WM_SVC.NAME", "Walmart Marketplace");
//            req.Headers.Add("WM_SEC.AUTH_SIGNATURE", sigStr);
//            req.Headers.Add("WM_CONSUMER.ID", ConsumerID);
//            req.Headers.Add("WM_SEC.TIMESTAMP", timeStamp.ToString());
//            req.Headers.Add("WM_QOS.CORRELATION_ID", Guid.NewGuid().ToString());
//            req.Headers.Add("WM_CONSUMER.CHANNEL.TYPE", ChannelType);
//            return req;
//        }

//        public Tuple<bool, string> RequestUpdatePriceItem(ListingWalmartViewModel model)
//        {
//            // string strBaseUrl = string.Format("https://marketplace.walmartapis.com/v3/price");
//            string strBaseUrl = string.Format("https://marketplace.walmartapis.com/v3/feeds?feedType=price");
//            string strRequestMethod = "POST";
//            WallmartSigner Signature = new WallmartSigner(this.ConsumerID, this.PrivateKey, strBaseUrl, strRequestMethod);
//            string strSignature = string.Empty;
//            HttpWebRequest httpWebRequest = CreateRequestObject(strBaseUrl, strRequestMethod);
//            //null;
//            HttpWebResponse httpWebResponse = null;
//            //WalmartOrderModel orderModel = new WalmartOrderModel();
//            StringBuilder strBuilder = new StringBuilder();
//            string responseStr = "";
//            Tuple<bool, string> ResponseString;
//            try
//            {

//                //        string strRequestBody =
//                //@"--BOUNDARYCommercebit \r\n<?xml version=""1.0"" encoding=""UTF-8""?>
//                //        <Price xmlns=""http://walmart.com/"">
//                //        <itemIdentifier>
//                //        <sku>" + model.SKU + @" </sku>
//                //        </itemIdentifier>
//                //        <pricingList>
//                //        <pricing>
//                //        <currentPrice>
//                //        <value currency=""USD"" amount = " + model.Price + @"/>
//                //        </currentPrice>
//                //        </pricing>
//                //        </pricingList>
//                //        </Price>
//                //        --BOUNDARYCommercebit-- \r\n \r\n";
//                string strRequestBody =
//        @"--BOUNDARYCommercebit \r\n<?xml version=""1.0"" encoding=""UTF-8""?>
//                <PriceFeed xmlns:gmp='http://walmart.com/'>
//  <PriceHeader>
//    <version>1.5</version>
//  </PriceHeader>
//  <Price>
//    <itemIdentifier>
//      <sku>MRTSHIRT101</sku>
//    </itemIdentifier>
//    <pricingList>
//      <pricing>
//        <currentPrice>
//          <value currency='USD' amount='25.00'/>
//        </currentPrice>
//      </pricing>
//    </pricingList>
//  </Price>
//</PriceFeed>
//                --BOUNDARYCommercebit-- \r\n \r\n";
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write(strRequestBody);
//                writer.Flush();
//                stream.Position = 0;
//                httpWebRequest.ContentLength = stream.Length;
//                using (Stream requestStream = httpWebRequest.GetRequestStream())
//                {
//                    stream.CopyTo(requestStream);
//                    //requestStream.Write(stream, 0, stream.Length);
//                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
//                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
//                    {
//                        Stream streamResponse = httpWebResponse.GetResponseStream();
//                        //var responseResult = XDocument.Load(streamResponse);
//                        responseStr = new StreamReader(streamResponse).ReadToEnd();
//                        ResponseString = new Tuple<bool, string>(true, responseStr);
//                    }
//                    else
//                        ResponseString = new Tuple<bool, string>(false, responseStr);
//                }

//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                ResponseString = new Tuple<bool, string>(false, exMessage);
//            }
//            return ResponseString;
//        }

//        public Tuple<bool, string> RequestUpdateBulkPriceItem(List<ListingWalmartViewModel> model)
//        {
//            string strBaseUrl = string.Format("https://marketplace.walmartapis.com/v3/price");
//            string strRequestMethod = "POST";
//            WallmartSigner Signature = new WallmartSigner(this.ConsumerID, this.PrivateKey, strBaseUrl, strRequestMethod);
//            string strSignature = string.Empty;
//            HttpWebRequest httpWebRequest = CreateRequestObject(strBaseUrl, strRequestMethod);
//            //null;
//            HttpWebResponse httpWebResponse = null;
//            //WalmartOrderModel orderModel = new WalmartOrderModel();
//            StringBuilder strBuilder = new StringBuilder();
//            string responseStr = "";
//            Tuple<bool, string> ResponseString;
//            try
//            {
//                var StringBulkQty = "";
//                foreach (var item in model)
//                {
//                    StringBulkQty += "<Price>";
//                    StringBulkQty += "<itemIdentifier>";
//                    StringBulkQty += "<sku>'" + item.SKU + "'</sku>";
//                    StringBulkQty += "</itemIdentifier>";
//                    StringBulkQty += "<pricingList>";
//                    StringBulkQty += "<pricing>";
//                    StringBulkQty += "<currentPrice>";
//                    StringBulkQty += "<value currency ='USD' amount = '" + item.Price + "'/>";
//                    StringBulkQty += "</currentPrice>";
//                    StringBulkQty += "</pricing>";
//                    StringBulkQty += "</pricingList>";
//                    StringBulkQty += "</Price>";
//                }

//                string strRequestBody =
//        @"--BOUNDARYCommercebit \r\n<?xml version=""1.0"" encoding=""UTF-8""?>
//        <PriceFeed xmlns:gmp=""http://walmart.com/"">
//       <PriceHeader>
//       <version>1.5</version>
//       </PriceHeader>" + StringBulkQty + @"
//        </PriceFeed>
//        --BOUNDARYCommercebit-- \r\n \r\n";
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write(strRequestBody);
//                writer.Flush();
//                stream.Position = 0;
//                httpWebRequest.ContentLength = stream.Length;
//                using (Stream requestStream = httpWebRequest.GetRequestStream())
//                {
//                    stream.CopyTo(requestStream);
//                    //requestStream.Write(stream, 0, stream.Length);
//                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
//                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
//                    {
//                        Stream streamResponse = httpWebResponse.GetResponseStream();
//                        //var responseResult = XDocument.Load(streamResponse);
//                        responseStr = new StreamReader(streamResponse).ReadToEnd();
//                        ResponseString = new Tuple<bool, string>(true, responseStr);
//                    }
//                    else
//                        ResponseString = new Tuple<bool, string>(false, responseStr);
//                }

//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                ResponseString = new Tuple<bool, string>(false, exMessage);
//            }
//            return ResponseString;
//        }

//        public Tuple<bool, string> RequestUpdateQtyItem(ListingWalmartViewModel model)
//        {
//            string strBaseUrl = string.Format("https://marketplace.walmartapis.com/v2/inventory?sku={0}", model.SKU);
//            string strRequestMethod = "POST";
//            WallmartSigner Signature = new WallmartSigner(this.ConsumerID, this.PrivateKey, strBaseUrl, strRequestMethod);
//            string strSignature = string.Empty;
//            HttpWebRequest httpWebRequest = CreateRequestObject(strBaseUrl, strRequestMethod);
//            //null;
//            HttpWebResponse httpWebResponse = null;
//            //WalmartOrderModel orderModel = new WalmartOrderModel();
//            StringBuilder strBuilder = new StringBuilder();
//            string responseStr = "";
//            Tuple<bool, string> ResponseString;
//            try
//            {

//                string strRequestBody =
//        @"--BOUNDARYCommercebit \r\n<?xml version=""1.0"" encoding=""UTF-8""?>
//        <inventory xmlns:ns2=""http://walmart.com/"">
//        <sku>" + model.SKU + @"</sku>
//        <quantity>
//        <unit>EACH</unit>
//        <amount>" + model.Price + @"</amount>
//        </quantity>
//        <fulfillmentLagTime>1</fulfillmentLagTime>
//        </inventory>
//        --BOUNDARYCommercebit-- \r\n \r\n";
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write(strRequestBody);
//                writer.Flush();
//                stream.Position = 0;
//                httpWebRequest.ContentLength = stream.Length;
//                using (Stream requestStream = httpWebRequest.GetRequestStream())
//                {
//                    stream.CopyTo(requestStream);
//                    //requestStream.Write(stream, 0, stream.Length);
//                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
//                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
//                    {
//                        Stream streamResponse = httpWebResponse.GetResponseStream();
//                        //var responseResult = XDocument.Load(streamResponse);
//                        responseStr = new StreamReader(streamResponse).ReadToEnd();
//                        ResponseString = new Tuple<bool, string>(true, responseStr);
//                    }
//                    else
//                        ResponseString = new Tuple<bool, string>(false, responseStr);
//                }

//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                ResponseString = new Tuple<bool, string>(false, exMessage);
//            }
//            return ResponseString;
//        }

//        public Tuple<bool, string> RequestBulkUpdateQtyItem(List<ListingWalmartViewModel> model)
//        {
//            string strBaseUrl = string.Format("https://marketplace.walmartapis.com/v3/feeds?feedType=item");
//            string strRequestMethod = "POST";
//            WallmartSigner Signature = new WallmartSigner(this.ConsumerID, this.PrivateKey, strBaseUrl, strRequestMethod);
//            string strSignature = string.Empty;
//            HttpWebRequest httpWebRequest = CreateRequestObject(strBaseUrl, strRequestMethod);
//            //null;
//            HttpWebResponse httpWebResponse = null;
//            //WalmartOrderModel orderModel = new WalmartOrderModel();
//            StringBuilder strBuilder = new StringBuilder();
//            string responseStr = "";
//            Tuple<bool, string> ResponseString;

//            try
//            {
//                var StringBulkQty = "";
//                foreach (var item in model)
//                {
//                    StringBulkQty += "<inventory>";
//                    StringBulkQty += "<sku>'" + item.SKU + "'</sku>";
//                    StringBulkQty += "<quantity>";
//                    StringBulkQty += "<unit>EACH</unit>";
//                    StringBulkQty += "<amount>'" + item.Price + "'</amount>";
//                    StringBulkQty += "</quantity>";
//                    StringBulkQty += "<fulfillmentLagTime>1</fulfillmentLagTime>";
//                    StringBulkQty += "</inventory>";
//                }

//                string strRequestBody =
//         @"--BOUNDARYCommercebit \r\n<?xml version=""1.0"" encoding=""UTF-8""?>
//        <InventoryFeed xmlns=""http://walmart.com/"">
//        <InventoryHeader>
//        <version>1.4</version>
//        </InventoryHeader>
//        " + StringBulkQty + @"
//        </InventoryFeed>
//        --BOUNDARYCommercebit-- \r\n \r\n";
//                MemoryStream stream = new MemoryStream();
//                StreamWriter writer = new StreamWriter(stream);
//                writer.Write(strRequestBody);
//                writer.Flush();
//                stream.Position = 0;
//                httpWebRequest.ContentLength = stream.Length;
//                using (Stream requestStream = httpWebRequest.GetRequestStream())
//                {
//                    stream.CopyTo(requestStream);
//                    //requestStream.Write(stream, 0, stream.Length);
//                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
//                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
//                    {
//                        Stream streamResponse = httpWebResponse.GetResponseStream();
//                        //var responseResult = XDocument.Load(streamResponse);
//                        responseStr = new StreamReader(streamResponse).ReadToEnd();
//                        ResponseString = new Tuple<bool, string>(true, responseStr);
//                    }
//                    else
//                        ResponseString = new Tuple<bool, string>(false, responseStr);
//                }

//            }
//            catch (WebException ex)
//            {
//                string exMessage = ex.Message;

//                if (ex.Response != null)
//                {
//                    using (var responseReader = new StreamReader(ex.Response.GetResponseStream()))
//                    {
//                        exMessage = responseReader.ReadToEnd();
//                    }
//                }
//                ResponseString = new Tuple<bool, string>(false, exMessage);
//            }
//            return ResponseString;
//        }
//    }
//}