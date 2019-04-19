using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Lab08.MVC.Business;
using Lab08.MVC.Data;
using Lab08.MVC.Domain;
using Lab08.MVC.Mappers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Lab08.MVC
{
    public static class IoCBuilder
    {
        public static void Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataBaseContext>().As<IDataBaseContext>().InstancePerRequest();
            builder.RegisterType<DataBaseContext>().As<DbContext>().InstancePerRequest();

            builder.RegisterType<BookModelMapper>().As<IBookModelMapper>().InstancePerDependency();
            builder.RegisterType<UserModelMapper>().As<IUserModelMapper>().InstancePerDependency();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>().InstancePerDependency();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterType<UserProfileService>().As<IUserProfileService>().InstancePerDependency();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerDependency();

            builder.RegisterType<UserService>().AsSelf().InstancePerDependency();
            builder.RegisterType<UserStore<IdentityUserModel>>().As<IUserStore<IdentityUserModel>>().InstancePerDependency();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerDependency();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}