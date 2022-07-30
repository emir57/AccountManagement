using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnAfter(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation)
        {
            OnBefore(invocation);
            bool isSuccess = true;
            try
            {
                invocation.Proceed();
                var task = invocation.ReturnValue as Task;
                if (task != null)
                    if (task.IsFaulted)
                    {
                        OnException(invocation, task.Exception);
                    }
            }
            catch (Exception e)
            {
                OnException(invocation, e);
                isSuccess = false;
            }
            finally
            {
                if (isSuccess)
                    OnSuccess(invocation);
            }
            OnAfter(invocation);
        }
    }
}
