<<<<<<< HEAD
﻿using System.Net;
=======
﻿using Newtonsoft.Json;
using System.Net;
>>>>>>> Commit 08052017
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
<<<<<<< HEAD
using Newtonsoft.Json;
=======
>>>>>>> Commit 08052017

namespace Autenticacao.API.Models
{
	public class CustomMessage : IHttpActionResult
<<<<<<< HEAD
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Message { get; private set; }

        public static CustomMessage Create(HttpStatusCode statusCode, string message)
        {
            return new CustomMessage(statusCode, message);
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var resp = new CustomMessage(StatusCode, Message);
	        var response = new HttpResponseMessage(StatusCode)
	        {
		        Content =
			        new StringContent(JsonConvert.SerializeObject(resp), System.Text.Encoding.UTF8, "application/json")
	        };
	        return Task.FromResult(response);
        }

        private CustomMessage(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
=======
	{
		public HttpStatusCode StatusCode { get; private set; }
		public string Message { get; private set; }

		public static CustomMessage Create(HttpStatusCode statusCode, string message)
		{
			return new CustomMessage(statusCode, message);
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			var resp = new CustomMessage(StatusCode, Message);
			var response = new HttpResponseMessage(StatusCode)
			{
				Content =
					new StringContent(JsonConvert.SerializeObject(resp), System.Text.Encoding.UTF8, "application/json")
			};
			return Task.FromResult(response);
		}

		private CustomMessage(HttpStatusCode statusCode, string message)
		{
			StatusCode = statusCode;
			Message = message;
		}
	}
>>>>>>> Commit 08052017
}