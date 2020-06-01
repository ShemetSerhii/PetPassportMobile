using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PetPassport.Models.UserModels;

namespace PetPassport.Services
{
    public class ApiClient
    {
        public static User User { get; set; }

        protected const string Url = "http://192.168.75.1:45455/api";
        // настройка клиента

        protected HttpClient HttpClient
        {
            get
            {
                ServicePointManager.CheckCertificateRevocationList = false;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                return client;
            }
        }

        protected StringContent CreateContent(object content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        protected async Task<T> ReadContentAsync<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}