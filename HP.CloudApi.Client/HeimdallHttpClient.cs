using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace HeimdallPower
{
    internal class HeimdallHttpClient
    {
        protected HttpClient HttpClient { get; }
        private readonly ClientAssertionCertificate _certificate;
        private readonly string _scope;

        private const string Authority = "https://login.microsoftonline.com/132d3d43-145b-4d30-aaf3-0a47aa7be073";
        private const string ProdApiUrl = "https://api.heimdallcloud.com";
        private const string DevApiUrl = "https://api.heimdallcloud-dev.com";
        private const string ProdScope = "aac6dec0-4c1b-4565-a825-5bb9401a1547/.default";
        private const string DevScope = "6b9ba5c0-4a21-4263-bbf5-8c4e30c0ee1b/.default";

        private DateTimeOffset _tokenExpiresOn;

        public HeimdallHttpClient(bool useDeveloperApi)
        {
            _scope = useDeveloperApi ? DevScope : ProdScope;
            var apiUrl = useDeveloperApi ? DevApiUrl : ProdApiUrl;
            HttpClient = new() { BaseAddress = new Uri(apiUrl) };
        }

        public HeimdallHttpClient(string clientId, bool useDeveloperApi, string pfxCertificatePath, string certificatePassword) : this(useDeveloperApi)
        {
            var certPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pfxCertificatePath);
            var certfile = File.OpenRead(certPath);
            var certificateBytes = new byte[certfile.Length];
            certfile.Read(certificateBytes, 0, (int)certfile.Length);
            var x509Certificate2 = new X509Certificate2(
                certificateBytes,
                certificatePassword,
                X509KeyStorageFlags.Exportable |
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet);
            _certificate = new ClientAssertionCertificate(clientId, x509Certificate2);
        }

        public HeimdallHttpClient(string clientId, bool useDeveloperApi, X509Certificate2 certificate): this(useDeveloperApi)
        {
            _certificate = new ClientAssertionCertificate(clientId, certificate);            
        }

        public async Task<T> Get<T>(string url)
        {
            await UpdateAccessTokenIfExpired();
            var response = await HttpClient.GetAsync(url);

            var jsonString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<T>(jsonString);
                return result;
            }

            dynamic parsedJson = JsonConvert.DeserializeObject(jsonString);
            var exceptionString = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            Console.WriteLine($"Request failed, exception: {exceptionString}");

            return default;
        }

        private async Task UpdateAccessTokenIfExpired()
        {
            if (DateTime.Now > _tokenExpiresOn)
            {
                AuthenticationContext context = new AuthenticationContext(Authority);
                AuthenticationResult authenticationResult = await context.AcquireTokenAsync(_scope, _certificate);
                _tokenExpiresOn = authenticationResult.ExpiresOn;
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            }
        }
    }
}