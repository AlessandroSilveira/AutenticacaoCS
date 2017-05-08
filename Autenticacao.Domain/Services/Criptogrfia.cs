using System;
using System.Text;
using Autenticacao.Domain.Interfaces.Service;

namespace Autenticacao.Domain.Services
{
	public class Criptografia : ICriptografia
	{
		public static string Hash(string senha)
		{
			var bytes = new UTF8Encoding().GetBytes(senha);
			byte[] hashBytes;
			using (var algorithm = new System.Security.Cryptography.SHA512Managed())
			{
				hashBytes = algorithm.ComputeHash(bytes);
			}

			return Convert.ToBase64String(hashBytes);
		}

		string ICriptografia.Hash(string senha)
		{
			return Hash(senha);
		}
	}
}
