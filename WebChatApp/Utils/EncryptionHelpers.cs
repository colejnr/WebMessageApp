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
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
					{
						swEncrypt.Write(rawMessage);
					}

					byte[] iv = aesAlg.IV;
					byte[] encryptedContent = msEncrypt.ToArray();
					byte[] combinedIvAndEncryptedContent = new byte[iv.Length + encryptedContent.Length];
					Array.Copy(iv, 0, combinedIvAndEncryptedContent, 0, iv.Length);
					Array.Copy(encryptedContent, 0, combinedIvAndEncryptedContent, iv.Length, encryptedContent.Length);

					return Convert.ToBase64String(combinedIvAndEncryptedContent);
				}
			}
		}


