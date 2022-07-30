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

namespace Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionLogAspect(Type loggingService)
        {
            if (loggingService.BaseType != typeof(LoggerServiceBase))
            {
                throw new WrongLoggingTypeException();
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggingService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            string logDetailWithException = GetLogDetail(invocation, e);

            _loggerServiceBase.Error(logDetailWithException);
        }

        private string GetLogDetail(IInvocation invocation, System.Exception e)
        {
            var parameters = invocation.Arguments.Select((t, i) => new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Type = invocation.Arguments[i].GetType().ToString(),
                Value = invocation.Arguments[i]
            });

            LogDetailWithException logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.GetConcreteMethod().Name,
                Parameters = parameters.ToList(),
                ExceptionMessage = e.Message,
                UserEmail = _httpContextAccessor.HttpContext.User.ClaimEmail()
            };
            return JsonConvert.SerializeObject(logDetailWithException);
        }
    }
}
