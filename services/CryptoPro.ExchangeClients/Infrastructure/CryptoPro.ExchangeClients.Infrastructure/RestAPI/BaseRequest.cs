﻿using CryptoPro.ExchangeClients.Infrastructure.RestAPI.Options;
using RestSharp;

namespace CryptoPro.ExchangeClients.Infrastructure.RestAPI.Requests;

public abstract class BaseRequest
{
    public RestClient? Client;
    public RequestOptions? RequestOptions { get; set; }
    public ExchangeApiOptions? ApiOptions { get; set; }
    public abstract BaseRequest Authorize(Dictionary<string, object>? bodyParameters = null);
    
    public async Task<string> ExecuteAsync()
    {
        if (RequestOptions == null || Client == null)
            throw new NullReferenceException("[Request error] : First you need to execute 'Create' method.");
        
        var result = (await Client
            .ExecuteAsync(RequestOptions.Request, RequestOptions.Request.Method))
            .Content;
        
        if (string.IsNullOrEmpty(result))
            throw new Exception("[Request error] Request fetch error.");
        
        return result;
    }
}