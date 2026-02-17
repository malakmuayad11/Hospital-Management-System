using Hospital_Business;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_System
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public readonly static string DateFormat = "dd/MM/yyyy";

        internal readonly static string Key = "1234567890123456";
        internal readonly static string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\Hospital";

        private readonly static string apiKey = "ZNVAkivOi5Qr3XPpILUSHLB0DkpigwYR";
        private readonly static string apiUrl = "https://rest.smsmode.com/sms/v1/messages";

        public static string Encrypt_AES(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt_AES(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public static void WriteInRegistry(string ValueName, string ValueData)
        {
            try
            {
                Registry.SetValue(KeyPath, ValueName, ValueData, RegistryValueKind.String);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public static string ReadFromRegistry(string ValueName)
        {
            try
            {
                return Registry.GetValue(KeyPath, ValueName, null) as string;
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return null;
        }    

        public static async Task<bool> SendSmsAsync(string phoneNumber, string message)
       {
           using (var client = new HttpClient())
           {
               // Set headers
               client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
               client.DefaultRequestHeaders.Add("Accept", "application/json");

                // Convert phone number to E.164 format
                phoneNumber = "+" + phoneNumber.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty);

               // Build JSON payload
               var payload = new
               {
                   recipient = new { to = phoneNumber },
                   body = new { text = message }
               };

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

               // Send POST request
               var response = await client.PostAsync(apiUrl, content);
               return response.IsSuccessStatusCode;
           }
       }
    }
}