using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace StringEncryptionTest_ConsoleApp1
{
	public class RijndaelExample
	{
		public static void Main()
		{
			try
			{

				string key = "test1234".PadRight(32, '9'); // length = 32
				string vi = "4321test".PadRight(16, '9'); // length = 16
				string original = "Here is some data to encrypt!";

				// Create a new instance of the RijndaelManaged
				// class.  This generates a new key and initialization 
				// vector (IV).
				//using (RijndaelManaged crypt = new RijndaelManaged())
				//{

				//	crypt.GenerateKey();
				//	crypt.GenerateIV();
				// Encrypt the string to an array of bytes.
				//byte[] encrypted = EncryptStringToBytes(original, crypt.Key, crypt.IV);
				byte[] encrypted = EncryptStringToBytes(original, Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(vi));

				// Decrypt the bytes to a string.
				//string roundtrip = DecryptStringFromBytes(encrypted, crypt.Key, crypt.IV);
				string roundtrip = DecryptStringFromBytes(encrypted, Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(vi));

				//Display the original data and the decrypted data.
				Console.WriteLine("Original:   {0}", original);
				Console.WriteLine("Round Trip: {0}", roundtrip);
				//}

			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e.Message);
			}
		}

		public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");
			byte[] encrypted;
			// Create an RijndaelManaged object
			// with the specified key and IV.
			using (RijndaelManaged rijAlg = new RijndaelManaged())
			{
				rijAlg.Key = Key;
				rijAlg.IV = IV;

				// Create a decrytor to perform the stream transform.
				ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{

							//Write all data to the stream.
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}


			// Return the encrypted bytes from the memory stream.
			return encrypted;

		}

		public static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			// Check arguments.
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			// Create an RijndaelManaged object
			// with the specified key and IV.
			using (RijndaelManaged rijAlg = new RijndaelManaged())
			{
				rijAlg.Key = Key;
				rijAlg.IV = IV;

				// Create a decrytor to perform the stream transform.
				ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

				// Create the streams used for decryption.
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{

							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}

			}

			return plaintext;

		}

	}
}