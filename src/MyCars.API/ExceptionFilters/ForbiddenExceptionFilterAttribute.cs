using MyCars.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace MyCars.API.ExceptionFilters
{
    public class ForbiddenExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if(context.Exception is ForbiddenException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}