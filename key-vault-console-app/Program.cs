using System;
using System.IO;
using NDesk.Options;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Azure.KeyVault;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.WebKey;
using Microsoft.Identity.Client;
using System.Linq;
using System.Threading;
using System.Net.Http;
using Azure.Security.Keys;
using Azure.Identity;
using System.Net;
using Newtonsoft.Json.Linq;
using Azure.Security.KeyVault.Keys.Cryptography;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Text;

namespace key_vault_console_app
{
    class Program
    {
        static readonly string Directory_Tenant_ID = "884bae33-030b-4d2e-9c7f-d2f5522f5c17";
        static async Task Main(string[] args)
        {
            var keyVaultName = "kollmorgenTest";
            var keyVaultKeyName = "PrimaryKey";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            /*            var client = new KeyClient(new Uri(kvUri), creds);
                        Console.WriteLine($"Retrieving your key from {keyVaultName}.");
                        var key = await client.GetKeyAsync("PrimaryKey");
                        Console.WriteLine($"Your key version is '{key.Value.Properties.Version}'.");
                        Console.ReadLine();*/
            string args2 = "TWGVYB4LjfKR0LjKvYRtRwvhXHaUneSo6JD0zOl4wA9Lwaex/6RnabUea11CJOSGmIp9VIGoIbEOzBGcklQtkAqfuXm8T73pQD0lit9MyqbIwmvKN+3coZRRIriuup1PqGsm/OSsA1YO6EGHyVFFNIF5I1oxEOYCzWsWtzlEL8blpxn7g04ZVfsj7llGm9YEmH52JNilcgHM0Nlpe//WlLxH+Csim4JSRlth6k51GqJQYfGieGWYQtf2YeqnoBU6rjcSQiHzm+RlfqWRiqbERGHX1wuXYbl2yaGu6S392ny4+45WxxezAQjEMuTvWqOJhtnUxSR6Y2pTOZ71ITgX8w==";
            //PwdSuperuser.PwdSuperuser.start(args2);
            /*
                        var cc = new CryptographyClient(new Uri(kvUri), creds);

                        var binary_data = System.Convert.FromBase64String(args2);
                        DecryptResult decryptResult = await cc.DecryptAsync(EncryptionAlgorithm.Rsa15, binary_data);
                        Console.Write(Encoding.Default.GetString(decryptResult.Plaintext));
                        Console.ReadLine();*/

            var client = new KeyClient(new Uri(kvUri), new DefaultAzureCredential());
            var key = await client.GetKeyAsync(keyVaultKeyName);
            CryptographyClient cryptoClient = new CryptographyClient(key.Value.Id, new DefaultAzureCredential());
            Console.WriteLine(args2);
            Console.WriteLine("-------------dencrypt---------------");
            var encryptedBytes = Convert.FromBase64String(args2);
            var dencryptResult = await cryptoClient.DecryptAsync(EncryptionAlgorithm.Rsa15, encryptedBytes);
            var decryptedText = Encoding.Unicode.GetString(dencryptResult.Plaintext);
            Console.WriteLine(decryptedText);
            Console.ReadLine();
        }
    }
}
