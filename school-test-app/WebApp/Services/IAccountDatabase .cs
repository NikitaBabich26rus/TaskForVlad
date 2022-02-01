using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IAccountDatabase
    {
        Task<Account> GetOrCreateAccountAsync(string id);

        Task<Account> GetOrCreateAccountAsync(long id);

        Task<Account> FindByUserNameAsync(string userName);

        Task ResetAsync();
    }
}