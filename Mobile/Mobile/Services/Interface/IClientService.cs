using System.Net.Http;
using System.Threading.Tasks;

namespace Mobile.Services.Interface
{
    public interface IClientService<T>
    {
        void Initialize();
        //void Token();
        Task<HttpResponseMessage> CreateEntityAsync(T entity, string url = "");
        Task<HttpResponseMessage> UpdateEntityAsync(T entity, string url = "");
        Task<HttpResponseMessage> LoginAsync(T entity, string url = "");
    }
}