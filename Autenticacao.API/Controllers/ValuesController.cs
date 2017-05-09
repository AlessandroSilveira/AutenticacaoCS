using System.Web.Http;

namespace Autenticacao.API.Controllers
{
    public class ValuesController : ApiController
    {
        [Authorize()]
        public string Get()
        {
            return "O usuário logado é " + User.Identity.Name;
        }
    }
}
