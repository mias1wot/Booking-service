using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BookingServiceApp.Application.Helpers
{
	public static class DSAImplementation
	{
		// It is your responisibility to call Dispose() on DSACryptoServiceProvider object.
		public static DSACryptoServiceProvider GenerateKeys()
		{
			DSACryptoServiceProvider DSA = new DSACryptoServiceProvider();
			return DSA;
		}


		public static byte[] CreateSignature(byte[] text, DSACryptoServiceProvider dsaInstance)
		{
			byte[] signature = dsaInstance.SignData(text);
			return signature;
		}

		public static bool VerifySignature(byte[] text, byte[] signature, DSACryptoServiceProvider dsaInstance)
		{
			bool isValid = dsaInstance.VerifyData(text, signature);
			return isValid;
		}


		public static void ExportKey(DSACryptoServiceProvider rsaInstance, bool includePrivateParameters, string filePath)
		{
			string key = rsaInstance.ToXmlString(includePrivateParameters);
			File.WriteAllText(filePath, key);
		}

		public static DSACryptoServiceProvider ImportKey(string filePath)
		{
			string key = File.ReadAllText(filePath);
			DSACryptoServiceProvider dsaInstance = new DSACryptoServiceProvider();
			dsaInstance.FromXmlString(key);
			return dsaInstance;
		}
	}
}
