using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation:MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation()
        {            
            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var userIdentity = _httpContextAccessor.HttpContext.User.Identity;

            if (userIdentity.IsAuthenticated)
            {
                return;
            }

            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
