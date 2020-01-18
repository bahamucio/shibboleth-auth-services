using System.Threading.Tasks;

namespace Shiboleth.Authentication
{
    public interface IUserRepository
    {
        Task<UserInfo> FindAsync(string name);
    }
}
