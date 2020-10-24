using System;
using System.Security.Cryptography;
using System.Text;

namespace SmartCooking.Common.Cryptography
{
	public static class CrypterAlgorithm
	{
		public static string ComputeMD5Hash(string input)
		{
			using(MD5 md5 = MD5.Create())
			{
				return ComputeHashAlgorithm(md5, input);
			}
		}
		
		public static string ComputeSha256Hash(string input) 
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				return ComputeHashAlgorithm(sha256, input);
			}
		}

		public static string ComputeSha512Hash(string input)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return ComputeHashAlgorithm(sha512, input);
			}
		}

		private static string ComputeHashAlgorithm(HashAlgorithm hashAlgorithm, string input)
		{
			byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("X2"));
			}
			return sBuilder.ToString();
		}

		public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
		{
			var hashOfInput = ComputeHashAlgorithm(hashAlgorithm, input);

			StringComparer comparer = StringComparer.OrdinalIgnoreCase;
			return comparer.Compare(hashOfInput, hash) == 0;
		}

	}
}
