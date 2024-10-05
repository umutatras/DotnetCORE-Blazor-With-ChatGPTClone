namespace ChatGPTClone.Application.Common.Models.Identity;

public class IdentityRefreshTokenResponse
{
    public string AccessToken { get; set; }
    public DateTime Expires { get; set; }
    public IdentityRefreshTokenResponse()
    {
        
    }
    public IdentityRefreshTokenResponse(string acccessToken,DateTime expires)
    {
        AccessToken = acccessToken;
        Expires = expires;
    }
}
