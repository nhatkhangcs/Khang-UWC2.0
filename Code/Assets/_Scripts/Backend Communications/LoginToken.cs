using Newtonsoft.Json;

public class LoginToken
{
    [JsonProperty("accessToken")]
    public string AccessToken { get; set; }
    
    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
}