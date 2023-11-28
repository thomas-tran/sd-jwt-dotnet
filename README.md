# SD-JWT-DotNet

## Overview

This is the reference implementation of [IETF SD-JWT specification](https://datatracker.ietf.org/doc/draft-ietf-oauth-selective-disclosure-jwt/) written in .Net 6,7 and 8.

### Features

- Create a Disclosure.
- Parse a Disclosure from Base64Url
- Create an SD-JWT.
- Parse an SD-JWT.
- Create a Dictionary instance that contains the "\_sd" array.
- Create a Dictionary instance that represents a selectively-disclosable
  array element.
- Encode a map or a list recursively.
- Decode a map or a list recursively.
- Support Decoy
- Support Algorithm SHA-1, MD5, SHA-256 , SHA-384 and SHA-512
- Support .Net 6 and 7

### Roadmap

- Abstract issuer, holder and verifier interfaces
- Support Algorithm SHA3-256, SHA3-384, and SHA3-512
- Support .Net 8
- Remove base64url dependencies

## Quickstart Guide

### Prerequisites

- Install .Net 6/7
- Visual Studio 2022 Community version
- Visual Studio Code with C# Dev Kit

### Installation

#### Window users

```
PM > Install-Package Owf.Sd.Jwt
```

#### Mac/Linux users

```
dot net add package Owf.Sd.Jwt
```

## Concepts

### Disclosure

In the context of the Selective Disclosure for JWTs (SD-JWT) specification, a disclosure is a cryptographic construction that enables the selective revelation of individual elements within a JSON object used as the payload of a JSON Web Signature (JWS) structure. It serves as a critical component of the SD-JWT mechanism, allowing parties to selectively disclose specific claims or portions of claims while maintaining the overall integrity and authenticity of the JWT.

A disclosure consists of three primary components:

- Salt: A random value generated by the issuer to ensure uniqueness and prevent replay attacks. The SD-JWT specification recommends that a salt have 128-bit or higher entropy and be base64url-encoded.

- Claim Name: The name of the claim or array element to be disclosed.

- Claim Value: The value of the claim or array element to be disclosed, transformed using a cryptographic hash function.

By combining these components, the issuer can create disclosures for specific claims or array elements within the JWT payload. These disclosures are then embedded within the SD-JWT, enabling the controlled revelation of sensitive information.

Create a Disclosure instance with salt, claimName and claimValue

```csharp
// Code Snippet 1: Create a Disclosure

// Parameters
var salt = "_26bc4LT-ac6q2KI6cBW5es";
var claimName = "family_name";
var claimValue = "Möbius";

// Create a Disclosure instance with salt, claimName and claimValue
var disclosure = Disclosure.Create(salt, claimName, claimValue);

// Get the string representation of the Disclosure.
// var disclosureString = disclosure.ToString();
var disclosureBase64Url = disclosure.GetBase64Url();
```

Create a Disclosure instance with claimName and claimValue. A salt will be generated automatically.

```csharp
// Code Snippet 2: Create a Disclosure without salt

// Parameters
var claimName = "family_name";
var claimValue = "Möbius";

// Create a Disclosure instance with claimName and claimValue
// A salt will be generated automatically
var disclosure = Disclosure.Create(claimName, claimValue);

// Get the string representation of the Disclosure.
// var disclosureString = disclosure.ToString();
var disclosureBase64Url = disclosure.GetBase64Url();

```

Create a Disclosure from the Base64Url string representation

```csharp
// Code Snippet 4: Create a Disclosure from Base64Url

// Parameters
var base64UrlDisclosure = "WyJfMjZiYzRMVC1hYzZxMktJNmNCVzVlcyIsICJmYW1pbHlfbmFtZSIsICJNw7ZiaXVzIl0";

// Create a Disclosure instance from base64Url
var disclosure = Disclosure.CreateFromBase64Url(base64UrlDisclosure);
```

### Disclosure Digest

In the Selective Disclosure for JWTs (SD-JWT) specification, the primary purpose of the disclosure digest is to provide a means of verifying the integrity and authenticity of the disclosed information without requiring access to the full JWT payload. This is particularly useful in scenarios where the full JWT payload may contain sensitive or confidential information that should not be exposed to unauthorized parties.

The fundamental concept of selectively disclosing claims in a JWT involves removing specific claims from the JWT and replacing them with their corresponding digest values. These digest values are then transmitted alongside the JWT. Since the original claims cannot be directly inferred from their digest counterparts, a recipient cannot solely rely on the JWT to determine the actual claim values. Instead, they must receive the corresponding disclosures along with the JWT to ascertain the actual claim values.

This diagram illustrates the flow of information in the selective disclosure process for JWTs

```mermaid
flowchart LR
A[JWT] --> B{Digest Values}
B --> C{Disclosures}
C --> D[Actual Claims]
A --> D
```

1. The JWT contains the original claims.
2. The digest values of the claims are extracted from the JWT.
3. The disclosures which contain the actual claim values, are sent separately from the JWT.
4. The recipient can obtain the actual claims by combining the digest values and the corresponding disclosures.

When a digest value of Disclosure is embedded in a JSON object, it is listed as an element in the "\_sd" array. The name of the array, i.e., "\_sd", is defined in the SD-JWT specification for the purpose.

For example, a "family_name" claim in a JSON object like below

```json
{
  "family_name": "Möbius"
}
```

will be replaced with a digest value like below

```json
{
  "_sd": ["TZjouOTrBKEwUNjNDs9yeMzBoQn8FFLPaJjRRmAtwrM"]
}
```

Compute a Disclosure digest using default algorithm SHA-256

```C#
// Code Snippet 5: Disclosure digest with default algorithm SHA-256

var digest = disclosure.Digest();
```

Compute a Disclosure digest using other algorithms such SHA-512 or MD5

```C#
// Code Snippet 6: Disclosure digest with other supported algorithms

// SupportHashAlgorithm.MD5
// SupportHashAlgorithm.SHA384
var digest = disclosure.Digest(SupportHashAlgorithm.SHA512);
```

## Join the community

- Connect and get latest updates: [Discord](https://discord.gg)
- Get help, request features and report bugs: [Github Discussions](https://)

## License

Apache License, Version 2.0

## Credits

Thanks to Takahiko Kawasaki to create the original [source code](https://github.com/authlete/sd-jwt) in Java version.
