using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.Exceptions.Aspect;
using Core.Extensions.Jwt;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Reflection;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogAspect(Type loggingService)
        {
            if (loggingService.BaseType != typeof(LoggerServiceBase))
            {
                throw new WrongLoggingTypeException();
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggingService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase?.Info(GetLogDetail(invocation));
        }

        private string GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Type = invocation.Arguments[i].GetType().ToString(),
                    Value = checkPasswordProperty(invocation.Arguments[i]) ? "***" : invocation.Arguments[i]
                });
            }
            LogDetail logDetail = new LogDetail
            {
                MethodName = invocation.GetConcreteMethod().Name,
                Parameters = logParameters,
                UserEmail = _httpContextAccessor.HttpContext.User.ClaimEmail()
            };
            return JsonConvert.SerializeObject(logDetail);
        }

        private bool checkPasswordProperty(object obj)
        {
            var objType = obj.GetType();
            if (objType.GetProperty("Password") != null)
                return true;
            if (objType.GetProperty("PasswordHash") != null)
                return true;
            if (objType.GetProperty("PasswordSalt") != null)
                return true;
            return false;
        }

    }
}
