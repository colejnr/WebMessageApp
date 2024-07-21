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

		public static string Decrypt(string encryptedMessage, string key)
		{
			byte[] fullCipher = Convert.FromBase64String(encryptedMessage);

			using (Aes aesAlg = Aes.Create())
			{
				byte[] iv = new byte[aesAlg.BlockSize / 8];
				byte[] cipherText = new byte[fullCipher.Length - iv.Length];

				Array.Copy(fullCipher, iv, iv.Length);
				Array.Copy(fullCipher, iv.Length, cipherText, 0, cipherText.Length);

				aesAlg.Key = GetHashedKey(key);
				aesAlg.IV = iv;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				using (StreamReader srDecrypt = new StreamReader(csDecrypt))
				{
					return srDecrypt.ReadToEnd();
				}
			}
		}

		private static byte[] GetHashedKey(string key)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
			}
		}
	}
}

