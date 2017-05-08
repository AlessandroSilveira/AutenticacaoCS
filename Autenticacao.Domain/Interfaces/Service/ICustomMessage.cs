using Autenticacao.Domain.Interfaces.Service;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Autenticacao.Domain.Interfaces.Service
{
	public interface ICustomMessage
	{
		CustomMessage Create(HttpStatusCode statusCode, string message);
	
		Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken);
	}
}