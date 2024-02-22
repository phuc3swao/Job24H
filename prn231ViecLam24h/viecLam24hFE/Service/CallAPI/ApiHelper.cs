using System.Text;
using System.Text.Json;

namespace viecLam24hFE.Service.CallAPI
{
    public class ApiHelper
    {
        private static HttpClient httpClient = new HttpClient();

        //Generic method call Get API
        public static async Task<T> GetAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                return JsonSerializer.Deserialize<T>(jsonString, options);
            }
            else
            {
                throw new Exception("Failed to retrieve data from the API.");
            }
        }

        //Generic method call Get Post
        public static async Task PostAsync<T>(string url, object content)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception("Failed to post data to the API.");
            }
        }

        //Generic method call Get Put
        public static async Task PutAsync<T>(string url, object content)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                
            }
            else
            {
                throw new Exception("Failed to update data through the API.");
            }
        }

        //Generic method call Get Post
        public static async Task<bool> DeleteAsync(string url)
        {
            var response = await httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
