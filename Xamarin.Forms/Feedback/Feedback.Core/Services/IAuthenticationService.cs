using System.Threading.Tasks;
using Feedback.Core.Entities;

namespace Feedback.Core.Services
{
    public interface IAuthenticationService
    {
        User CurrentUser { get; }
        Task LoginWithFacebookAsync();
        void RestoreSession();
        Task LogoutAsync();
    }
}