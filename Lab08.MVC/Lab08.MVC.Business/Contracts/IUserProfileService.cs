using Lab08.MVC.Domain;

namespace Lab08.MVC.Business
{
    public interface IUserProfileService
    {
        UserProfile GetUserById(string userId);

        string GetUserTypeName(UserProfile user);

        void UpdateUser(UserProfile user);

        void DeleteUser(UserProfile user);

        void AddUser(UserProfile user);

        string GetUserLoginById(string userId);
    }
}