using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyCars.API.Models
{
    public class ExceptionResult : IHttpActionResult
    {
        public  string Message { get; private set; }
        public ExceptionResult(string message)
        {
            Message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(Message);

            return Task.FromResult(response);
        }
    }
}