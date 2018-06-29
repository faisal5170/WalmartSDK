/*
Copyright (c) 2018-present, Walmart Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

//  ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code++. Version 4.4.0.7
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace Walmart.Sdk.Marketplace.V2.Payload.Feed
{
    using System;
    using System.Xml.Serialization;
    using System.Xml;
    using System.Collections.Generic;
    using Walmart.Sdk.Base.Primitive;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "4.4.0.7")]

    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType=true, Namespace="http://walmart.com/", TypeName="errors")]
    [XmlRootAttribute(Namespace="http://walmart.com/", IsNullable=false, ElementName="errors")]
    public class Errors : BasePayload, IErrorsPayload
    {
        [XmlElement("error", ElementName="error")]
        public List<V2.Payload.Feed.Error> Error { get; set; }

        public string RenderErrors()
        {
            var exceptionMessage = "";
            foreach (var error in Error)
            {
                var errorMessage = string.Format("[{0}] - {1}; {2}", error.Severity, error.Code, error.Description);
                exceptionMessage += Environment.NewLine + errorMessage;
            }

            return exceptionMessage;
        }

        /// <summary>
        /// Errors class constructor
        /// </summary>
        public Errors()
        {
            Error = new List<Error>();
        }
    }
}
