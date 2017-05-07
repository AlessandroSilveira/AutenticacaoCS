using System;
using System.Text;

namespace Atenticacao.Infra.Security
{
	public class Criptografia
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
	}
}
