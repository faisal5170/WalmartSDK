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
    using System.Xml.Serialization;
    using System.Xml;
    using System.Collections.Generic;
    using Walmart.Sdk.Base.Primitive;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Xsd2Code", "4.4.0.7")]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(Namespace="http://walmart.com/")]
    [XmlRootAttribute("MPNameValueAttribute")]
    public class MPNameValueAttribute : BasePayload
    {
        /// <summary>
        /// name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
        /// <summary>
        /// type
        /// </summary>
        [XmlElement("type")]
        public ValueType Type { get; set; }
        /// <summary>
        /// Set of values for the attribute
        /// </summary>
        [XmlElement("value", ElementName="value")]
        public List<MPAttributeValue> Value { get; set; }
    
        /// <summary>
        /// MPNameValueAttribute class constructor
        /// </summary>
        public MPNameValueAttribute()
        {
            Value = new List<MPAttributeValue>();
        }
    }
}
