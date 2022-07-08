using PresentationUI.Model;

namespace PresentationUI.Services.Interfaces
{
    public interface IManagingSelfFinanceServiceAPI
    {
        Task<string> GetContentFromApiAsync(string url);
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<HttpResponseMessage> CreateAsync(string url, object item);
        Task<HttpResponseMessage> UpdateAsync(string url, object item);
    }
}
