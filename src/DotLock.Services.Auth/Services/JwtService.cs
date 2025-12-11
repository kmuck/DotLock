using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace DotLock.Services.Auth.Services;

public class JwtService
{
    private readonly RSA _rsa;
    public string KeyId { get; }

    public JwtService()
    {
        _rsa = RSA.Create(2048);
        KeyId = Guid.NewGuid().ToString("N");
    }

    public string GenerateJwt(Dictionary<string, object> payload)
    {
        var header = new Dictionary<string, object>
        {
            ["alg"] = "RS256",
            ["typ"] = "JWT",
            ["kid"] = KeyId
        };

        string headerJson = JsonSerializer.Serialize(header);
        string payloadJson = JsonSerializer.Serialize(payload);

        string h = Base64UrlEncode(Encoding.UTF8.GetBytes(headerJson));
        string p = Base64UrlEncode(Encoding.UTF8.GetBytes(payloadJson));

        string unsigned = $"{h}.{p}";

        byte[] signature = _rsa.SignData(
            Encoding.UTF8.GetBytes(unsigned),
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );

        string s = Base64UrlEncode(signature);

        return $"{unsigned}.{s}";
    }

    public JsonElement GetJwks()
    {
        var p = _rsa.ExportParameters(false);

        var jwk = new
        {
            kty = "RSA",
            use = "sig",
            alg = "RS256",
            kid = KeyId,
            n = Base64UrlEncode(p.Modulus!),
            e = Base64UrlEncode(p.Exponent!)
        };

        var jwks = new { keys = new[] { jwk } };
        return JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(jwks))!;
    }

    private static string Base64UrlEncode(byte[] bytes)
        => Convert.ToBase64String(bytes).Replace("+","-").Replace("/","_").Replace("=", "");
}
