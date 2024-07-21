using System;
namespace WebChatApp.Utils
{
	using System;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;

	public class EncryptionHelper
	{
		public static string Encrypt(string rawMessage, string key)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = GetHashedKey(key);
				aesAlg.GenerateIV();


