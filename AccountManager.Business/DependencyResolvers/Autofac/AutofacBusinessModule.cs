using Autofac;
using concrete = AccountManager.Business.Concrete;
using @abstract = AccountManager.Business.Abstract;
using AccountManager.Data.Concrete.EntityFramework;
using AccountManager.Data.Abstract;
using Core.Utilities.Security.Jwt;
using AccountManager.Business.Concrete;
using AccountManager.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Autofac.Extras.DynamicProxy;
using Core.Utilities.Interceptors;

namespace AccountManager.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Business
            builder.RegisterType<concrete.AccountManager>().As<@abstract.IAccountService>();
            builder.RegisterType<concrete.PersonManager>().As<@abstract.IPersonService>();
            #endregion

            #region DataAccess
            builder.RegisterType<EfPersonDal>().As<IPersonDal>();
            builder.RegisterType<EfAccountDal>().As<IAccountDal>();
            #endregion

            #region Jwt
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            #endregion

            #region Auth
            builder.RegisterType<concrete.AuthManager>().As<@abstract.IAuthService>();
            #endregion

            #region HttpContextAccessor
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            #endregion

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions
                {
                    Selector = new AspectInterceptorSelector()
                });

        }
    }
}
