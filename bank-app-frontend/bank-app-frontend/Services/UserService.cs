using bank;
using Blazored.LocalStorage;
using Models;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System;


namespace Services
{
    public class UserService
    {
        private HttpClientService httpClientService;
        private ILocalStorageService localStorageService;
        public UserService(HttpClientService httpClientService, ILocalStorageService localStorageService)
        {
            this.httpClientService = httpClientService;
            this.localStorageService = localStorageService;
        }

        public async Task<T> LoginUser<T>(User user) where T: class
        {
            T userDetails = await httpClientService.PostDataRequest<User, T>(AppRoutes.LOGIN_URL, user, typeof(T));
            if(userDetails != null)
            {
                string serializedUserDetails = JsonConvert.SerializeObject(userDetails);

                // Deserialize the JSON string to an object of type T (either Applicant or Teller)
                T? deserializedUserDetails;

                if (user.UserType.Equals(UserType.CUSTOMER))
                {
                    deserializedUserDetails = JsonConvert.DeserializeObject<Applicant>(serializedUserDetails) as T;
                }
                else
                {
                    deserializedUserDetails = JsonConvert.DeserializeObject<Teller>(serializedUserDetails) as T;
                }
                await localStorageService.SetItemAsync<T>(Constants.USER_DETAIL_LOCAL_STORAGE_KEY, deserializedUserDetails);
                await localStorageService.SetItemAsStringAsync(Constants.USER_TYPE_LOCAL_STORAGE_KEY, user.UserType.ToString());
                return deserializedUserDetails;
            }
            return userDetails;
        }

        public async Task<Applicant> GetCustomerDetailFromLocalStorage()
        {
            return await localStorageService.GetItemAsync<Applicant>(Constants.USER_DETAIL_LOCAL_STORAGE_KEY);
        }
    }
}
