﻿namespace AuthenticationAPI.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName);
    }
}