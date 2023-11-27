namespace Owf.Sd.Jwt;
public enum SupportHashAlgorithm
{
    MD5
    , SHA1
    , SHA256
    , SHA384
    , SHA512
#if NET8_0
    , SHA3_256
    , SHA3_384
    , SHA3_512
#endif
}
