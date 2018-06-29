﻿/**
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Walmart.Sdk.Base;
using Walmart.Sdk.Base.Primitive;

namespace Walmart.Sdk.Marketplace.V3.Api.Exception
{
    public class ApiException: Base.Primitive.BaseException
    {
        public IErrorsPayload Details { get; private set; }
        public Base.Http.IResponse Response { get; private set; }

        protected ApiException(string message) : base(message)
        { }

        public static ApiException Factory(IErrorsPayload errorDetails, Base.Http.IResponse errorResponse)
        {
            var httpResponse = errorResponse.RawResponse;
            var exceptionMessage = string.Format("API Error Occured [{0} {1}]", ((int)httpResponse.StatusCode).ToString(), httpResponse.ReasonPhrase);
            exceptionMessage += errorDetails.RenderErrors();
            var exception = new ApiException(exceptionMessage);
            exception.Details = errorDetails;
            exception.Response = errorResponse;
            return exception;
        }
    }
}
