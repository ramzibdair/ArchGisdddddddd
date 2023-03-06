using Newtonsoft.Json;

namespace ArchGis
{
    public class EsriTokenResponse 
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresInMinutes { get; set; }
    }

    public class EsriClient : IEsriClient
    {
        private readonly HttpClient _httpClient;
        private readonly string tokenUrl = "https://www.arcgis.com/sharing/rest/oauth2/token"; //update
        private readonly string clientId = "add_client_id"; // update
        private readonly string clientSecret = "add_client_secret"; // update
        private readonly string grantType = "client_credentials";
        private readonly int expirationInMinutes = 120;

        public EsriClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<EsriTokenResponse> GetToken()
        {
            var url =
                $"{tokenUrl}?client_id={clientId}&client_secret={clientSecret}&grant_type={grantType}&expiration={expirationInMinutes}";
            var response = await _httpClient.PostAsync(url, null);
            var result =
                await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<EsriTokenResponse>(result);
            if (string.IsNullOrWhiteSpace(token.AccessToken)) // Esri does not respect HTTP status codes and will always return 200. It puts errors in the body. 
            {
                throw new Exception("Could not retrieve Esri Token");
            }
            return token;
        }
    }
}
