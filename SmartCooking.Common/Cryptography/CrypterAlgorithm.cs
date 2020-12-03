using System;
using System.Security.Cryptography;
using System.Text;

namespace SmartCooking.Common.Cryptography
{
	/// <summary>
	/// Κλάση για τον υπολογισμό κρυπτογράφησης για διάφορους αλγορίθμους.
	/// https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography?view=net-5.0
	/// </summary>
	public static class CrypterAlgorithm
	{
		/// <summary>
		/// Υπολογισμός αλγόριθμου κρυπτογράφησης MD5
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ComputeMD5Hash(string input)
		{
			using (MD5 md5 = MD5.Create())
			{
				return ComputeHashAlgorithm(md5, input);
			}
		}

		/// <summary>
		/// Υπολογισμός αλγόριθμου κρυπτογράφησης SHA-256
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ComputeSha256Hash(string input)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				return ComputeHashAlgorithm(sha256, input);
			}
		}

		/// <summary>
		/// Υπολογισμός αλγόριθμου κρυπτογράφησης SHA-512
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ComputeSha512Hash(string input)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return ComputeHashAlgorithm(sha512, input);
			}
		}

		/// <summary>
		/// Κεντρική μέθοδος υπολογισμού κρυπτογράφησης
		/// </summary>
		/// <param name="hashAlgorithm"></param>
		/// <param name="input"></param>
		/// <returns></returns>
		private static string ComputeHashAlgorithm(HashAlgorithm hashAlgorithm, string input)
		{
			byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
			StringBuilder sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("X2"));
			}
			return sBuilder.ToString();
		}

		/// <summary>
		/// Μέθοδος σύγκρισης κρυπτογραφημένου κειμένου με απλό κείμενο.
		/// </summary>
		/// <param name="hashAlgorithm"></param>
		/// <param name="input"></param>
		/// <param name="hash"></param>
		/// <returns></returns>
		public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
		{
			string hashOfInput = ComputeHashAlgorithm(hashAlgorithm, input);

			StringComparer comparer = StringComparer.OrdinalIgnoreCase;
			return comparer.Compare(hashOfInput, hash) == 0;
		}
	}
}