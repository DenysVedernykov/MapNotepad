using MapNotepad.Models;

namespace MapNotepad.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool Status { get; }

        User Profile { get; }

        bool CheckEmailForUse(string login);

        bool EmailMatching(string login);

        bool PasswordMatching(string password);

        bool Registration(User newUser);

        bool Login(string login, string password);

        void LogOut();
    }
}
