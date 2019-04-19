using System;
using Lab08.MVC.Data;
using Lab08.MVC.Domain;

namespace Lab08.MVC.Business
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public UserProfileService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
        }

        public string GetUserLoginById(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<UserProfile>().GetById(userId).Login;
            }
        }

        public string GetUserTypeName(UserProfile user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                var userType = unitOfWork.GetRepository<UserType>().GetById(user.UserTypeId);

                return userType.Name;
            }
        }

        public UserProfile GetUserById(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<UserProfile>().GetById(userId);
            }
        }

        public void AddUser(UserProfile user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<UserProfile>().Add(user);
                unitOfWork.Commit();
            }
        }

        public void UpdateUser(UserProfile user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<UserProfile>().Edit(user);
                unitOfWork.Commit();
            }
        }

        public void DeleteUser(UserProfile user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<UserProfile>().Remove(user);
                unitOfWork.Commit();
            }
        }
    }
}