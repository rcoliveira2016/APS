using System.Threading.Tasks;
namespace SpotifyAPI.Example.Interface.Core
{
    public interface IApi
    {
        Task<bool> Login();
        void Logout();
    }
}
