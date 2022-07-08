using PresentationUI.Services.Interfaces;

namespace PresentationUI.Services
{

    public class ManagingSelfFinanceServiceAPI : IManagingSelfFinanceServiceAPI
    {
        private readonly IConfiguration _configuration;
        private readonly string _urlApi;
        public ManagingSelfFinanceServiceAPI(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _urlApi = _configuration.GetConnectionString("APIDefaultConnection");
        }

        public async Task<string> GetContentFromApiAsync(string url)
        {
            HttpClient http = new();            
            HttpResponseMessage response = await http.GetAsync(_urlApi + url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            HttpClient http = new();              
            return await http.DeleteAsync(_urlApi + url);            
        }
        public async Task<HttpResponseMessage> CreateAsync(string url, object item)
        {
            HttpClient http = new();
            return await http.PutAsJsonAsync(_urlApi + url, item);            
        }

        public async Task<HttpResponseMessage> UpdateAsync(string url, object item)
        {
            HttpClient http = new();
            return await http.PostAsJsonAsync(_urlApi + url, item);
        }
    }
}
