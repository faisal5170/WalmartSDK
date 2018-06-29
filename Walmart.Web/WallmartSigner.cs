using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Walmart.Web
{
    public class WallmartSigner
    {
        /// <summary>
        /// Begining of time according to Java world.
        /// </summary>
        private static readonly DateTime JanuaryFirst1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Initializes a new instance of the <see cref="WallmartSigner"/> class. 
        /// Default Constructor for Object initializer
        /// </summary>
        public WallmartSigner()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WallmartSigner"/> class.
        /// </summary>
        /// <param name="consumerId">
        /// The Consumer ID provided by Developer Portal.
        /// </param>
        /// <param name="privateKey">
        /// The Base64 Encoded Private Key provided by Developer Portal.
        /// </param>
        /// <param name="requestUrl">
        /// The URL of API request being made.
        /// </param>
        /// <param name="requestMethod">
        /// For signer you'll need get
        /// </param>
        public WallmartSigner(string consumerId, string privateKey, string requestUrl, string requestMethod)
        {
            this.ConsumerId = consumerId;
            this.PrivateKey = privateKey;
            this.RequestUrl = requestUrl;
            this.RequestMethod = requestMethod;
        }

        /// <summary>
        /// Consumer ID provided by Developer Portal. Looks like GUID
        /// </summary>
        public string ConsumerId { get; set; }

        /// <summary>
        /// Longest string at developer portal
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Something like https://marketplace.walmartapis.com/v3/orders?createdStartDate=2016-08-16
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Default value for wallmart signing is GET
        /// </summary>
        [DefaultValue("GET")]
        public string RequestMethod { get; set; }

        /// <summary>
        /// Gets the time stamp used for the signature.
        /// </summary>
        public string TimeStamp { get; private set; }

        /// <summary>
        /// Get the signature based on the timestamp.  If the timestamp is null, create
        /// a new timestamp.
        /// </summary>
        /// <param name="timeStamp">
        /// The time stamp.
        /// </param>
        /// <returns>
        /// The calculate signature <see cref="string"/>.
        /// </returns>
        public string GetWallmartSignature(string timeStamp)
        {
            TimeStamp = timeStamp ?? GetTimestampInJavaMillis();

            // Append values into string for signing
            var message = ConsumerId + "\n" + RequestUrl + "\n" + RequestMethod.ToUpper() + "\n" + TimeStamp + "\n";

            RsaKeyParameters rsaKeyParameter;
            try
            {
                var keyBytes = Convert.FromBase64String(this.PrivateKey);
                var asymmetricKeyParameter = PrivateKeyFactory.CreateKey(keyBytes);
                rsaKeyParameter = (RsaKeyParameters)asymmetricKeyParameter;
            }
            catch (Exception)
            {
                throw new Exception("Unable to load private key");
            }

            var signer = SignerUtilities.GetSigner("SHA256withRSA");
            signer.Init(true, rsaKeyParameter);
            var messageBytes = Encoding.UTF8.GetBytes(message);
            signer.BlockUpdate(messageBytes, 0, messageBytes.Length);
            var signed = signer.GenerateSignature();
            var hashed = Convert.ToBase64String(signed);
            return hashed;
        }

        /// <summary>
        /// Get the TimeStamp as a string equivalent to Java System.currentTimeMillis
        /// </summary>
        /// <returns>
        /// Generated sign string
        /// </returns>
        public static string GetTimestampInJavaMillis()
        {
            var millis = (DateTime.UtcNow - JanuaryFirst1970).TotalMilliseconds;
            return Convert.ToString(Math.Round(millis), CultureInfo.InvariantCulture);
        }
    }

}