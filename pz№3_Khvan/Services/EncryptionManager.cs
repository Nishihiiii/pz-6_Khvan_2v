using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace pz_3_Khvan.Services
{
	public static class EncryptionManager
	{
		public static string GenerateSha256SecurityKey(string userSecretKey)
		{
			if (string.IsNullOrEmpty(userSecretKey))
			{
				return string.Empty;
			}

			using (SHA256 sha256Transformer = SHA256.Create())
			{
				byte[] rawBuffer = Encoding.UTF8.GetBytes(userSecretKey);

				byte[] calculatedDigest =
					sha256Transformer.ComputeHash(rawBuffer);

				string formattedResultString =
					BitConverter.ToString(calculatedDigest)
					.Replace("-", string.Empty);

				return formattedResultString;
			}
		}
	}
}
