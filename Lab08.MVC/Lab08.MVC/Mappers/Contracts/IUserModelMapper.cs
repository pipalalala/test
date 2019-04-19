using Lab08.MVC.Domain;

namespace Lab08.MVC.Mappers
{
    public interface IUserModelMapper
    {
        UserInfoModel GetUserInfoModel(UserProfile userProfile);
    }
}
