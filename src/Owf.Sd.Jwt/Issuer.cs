
using Owf.Sd.Jwt.Abstracts;

namespace Owf.Sd.Jwt;

public class Issuer : IIssuer
{
    public string Issue(Dictionary<string, object> userClaims, string issuerJwk, string? holderJwk = null, string? signAlgorithm = null, bool addDecoyClaims = false, Dictionary<string, object>? extraHeaders = null)
    {
        Dictionary<string, object> sdJwtPayload = new Dictionary<string, object>();

        ObjectEncoder objectEncoder = new ObjectEncoder();

        return "";
    }
}
