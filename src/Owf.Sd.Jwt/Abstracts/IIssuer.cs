namespace Owf.Sd.Jwt.Abstracts;

public interface IIssuer
{
    public string Issue(Dictionary<string, object> userClaims, string issuerJwk, string? holderJwk = null, 
        string? signAlgorithm = null, bool addDecoyClaims = false, Dictionary<string, object>? extraHeaders = null);
}
