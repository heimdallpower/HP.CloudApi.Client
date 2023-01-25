using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace HeimdallPower
{
    internal class HeimdallHttpClient
    {
        protected HttpClient HttpClient { get; }
        private readonly IConfidentialClientApplication MsalClient;
        private readonly string _scope;
        private readonly string _instance;
        private readonly string _domain;
        private readonly string _authority;

        private const string ProdApiUrl = "https://api.heimdallcloud.com";
        private const string DevApiUrl = "https://api.heimdallcloud-dev.com";

        private const string Policy = "B2C_1A_CLIENTCREDENTIALSFLOW";
        private const string ProdInstance = "https://hpadb2cprod.b2clogin.com";
        private const string DevInstance = "https://hpadb2cdev.b2clogin.com";
        private const string ProdDomain = "hpadb2cprod.onmicrosoft.com";
        private const string DevDomain = "hpadb2cdev.onmicrosoft.com";
        private const string ProdScope = $"https://{ProdDomain}/dc5758ae-4eea-416e-9e61-812914d9a49a/.default";
        private const string DevScope = $"https://{DevDomain}/f2fd8894-ae2e-4965-8318-e6c6781b5b80/.default";
        private const string ProdAuthority = $"{ProdInstance}/tfp/{ProdDomain}/{Policy}";
        private const string DevAuthority = $"{DevInstance}/tfp/{DevDomain}/{Policy}";

        private DateTimeOffset _tokenExpiresOn;

        public HeimdallHttpClient(string clientId, string clientSecret, bool useDeveloperApi)
        {
            _scope = useDeveloperApi ? DevScope : ProdScope;
            _instance = useDeveloperApi ? DevInstance: ProdInstance;
            _domain = useDeveloperApi ? DevDomain : ProdDomain;
            _authority = useDeveloperApi ? DevAuthority : ProdAuthority;
            var apiUrl = useDeveloperApi ? DevApiUrl : ProdApiUrl;
            HttpClient = new() { BaseAddress = new Uri(apiUrl) };
            MsalClient = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(_authority)
                .Build();
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
                AuthenticationResult authenticationResult = await MsalClient.AcquireTokenForClient(new string[] { _scope }).ExecuteAsync();
                _tokenExpiresOn = authenticationResult.ExpiresOn;
                Console.WriteLine(authenticationResult.AccessToken);
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
            }
        }
    }
}