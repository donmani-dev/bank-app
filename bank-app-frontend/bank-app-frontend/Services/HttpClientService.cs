using bank;
using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;


namespace Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private ToastMessageService toastService;

        public HttpClientService(HttpClient httpClient, ToastMessageService toastService)
        {
            this._httpClient = httpClient;
            this.toastService = toastService;
        }

        public async Task<TResponse> SendGetRequest<TResponse>(string endPointUrl, Type responseType) where TResponse : class
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, endPointUrl);
                request.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    
                    TResponse responseObject = JsonConvert.DeserializeObject(responseBody, responseType) as TResponse;
                    return responseObject;
                }
                else
                {
                    toastService.ShowToastMessage(ToastType.Danger, "ERROR!", $"Status code: {response.StatusCode} with\nmessage: {responseBody}");
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                toastService.ShowToastMessage(ToastType.Danger, "ERROR!", $"Exception occurred {ex.Message}");
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return default;
        }



        public async Task<TResponse> PostDataRequest<TRequest, TResponse>(string endPointUrl, TRequest data, Type responseType) where TResponse : class
        {
            try
            {
                string jsonRequestData = JsonConvert.SerializeObject(data);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endPointUrl);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {   
                    TResponse responseObject = JsonConvert.DeserializeObject(responseBody, responseType) as TResponse;
                    return responseObject;
                }
                else
                {
                    toastService.ShowToastMessage(ToastType.Danger, "ERROR!", $"Status code : {response.StatusCode} with message {responseBody}");
                    Console.WriteLine($"Status code : {response.StatusCode}");
                }
                return default;
            }
            catch (Exception ex)
            {
                toastService.ShowToastMessage(ToastType.Danger, "ERROR!", $"Exception occurred {ex.Message}");
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return default;
        }

        public async Task<TResponse> PutDataRequest<TRequest, TResponse>(string endPointUrl, TRequest data, Type responseType) where TRequest : class
            where TResponse : class
        {
            try
            {
                string jsonRequestData = JsonConvert.SerializeObject(data);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, endPointUrl);
                request.Headers.Add("Accept", "application/json");
                request.Content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TResponse responseObject = JsonConvert.DeserializeObject(responseBody, responseType) as TResponse;
                    return responseObject;
                }
                else
                {
                    toastService.ShowToastMessage(ToastType.Danger, "ERROR!", $"Status code : {response.StatusCode} with message {responseBody}");
                    Console.WriteLine($"Status code : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                toastService.ShowToastMessage(ToastType.Danger, "ERROR!", $"Exception occurred {ex.Message}");
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
            return default;
        }

    }
}
