﻿using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable.OAuth2.Infrastructure;

namespace TestApplicationRestsharp.Authentication
{
    public class RequestFactory : IRequestFactory
    {
        #region IRequestFactory implementation

        public RestSharp.Portable.IRestClient CreateClient()
        {
            return new RestClient();
        }

        public RestSharp.Portable.IRestRequest CreateRequest(string resource)
        {
            return new RestRequest(resource);
        }

        #endregion
    }
}

