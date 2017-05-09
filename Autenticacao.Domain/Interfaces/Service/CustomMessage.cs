using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace Autenticacao.Domain.Interfaces.Service
{
	public class CustomMessage : ICustomMessage, IHttpActionResult

	{
		public HttpStatusCode StatusCode { get; }
		public string Message { get; }

		public CustomMessage(HttpStatusCode statusCode, string message)
		{
			StatusCode = statusCode;
			Message = message;
		}
		
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

		CustomMessage ICustomMessage.Create(HttpStatusCode statusCode, string message)
		{
			return new CustomMessage(statusCode, message);
		}
	}


}
