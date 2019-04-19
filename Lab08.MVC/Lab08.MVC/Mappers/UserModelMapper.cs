using System;
using System.Linq;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Mappers
{
    public class UserModelMapper : IUserModelMapper
    {
        private readonly IBookModelMapper bookModelMapper;

        public UserModelMapper(IBookModelMapper bookModelMapper)
        {
            this.bookModelMapper = bookModelMapper ?? throw new ArgumentNullException(nameof(bookModelMapper));
        }

        public UserInfoModel GetUserInfoModel(UserProfile userProfile)
        {
            if (userProfile == null)
            {
                throw new ArgumentNullException(nameof(userProfile));
            }

            return new UserInfoModel
            {
                Id = userProfile.Id,
                Login = userProfile.Login,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                BooksCount = userProfile.BooksCount,
                Books = userProfile.BooksProfiles.Select(bookModelMapper.GetBookInfoModel)
            };
        }
    }
}