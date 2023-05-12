using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BookingServiceApp.Application.Helpers
{
	public class HashHelper
	{
        public static string GetSHA256Hash<T>(T input)
        {
            var inputJson = JsonConvert.SerializeObject(input);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(inputJson);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
