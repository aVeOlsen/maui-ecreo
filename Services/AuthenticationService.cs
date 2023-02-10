using CommunityToolkit.Mvvm.ComponentModel;
using MauiEcreoLib;
using MauiTemplateEcreo.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiTemplateEcreo.Services
{
    public class AuthenticationService : ObservableObject, IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;
        //private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IHttpClientFactory factory,
                             IConfiguration config)
        {
            _client = new HttpClient();
            _factory = factory;
            _config = config;
            //_logger = logger;
        }
        public async Task<ClaimsPrincipal> SignIn(string username, string password)
        {
            Uri uri = new Uri(string.Format(Constants.AuthenticationRestUrl, string.Empty));
            Dictionary<string, object> formData = new Dictionary<string, object>();
            formData.Add("Email", username);
            formData.Add("Password", password);

            //_client = _factory.CreateClient(uri.ToString());

            StringContent content = new StringContent(JsonSerializer.Serialize(formData), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                string responsefucked = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responsefucked);
                return null;
            }

            string responseString = await response.Content.ReadAsStringAsync();
            JsonDocument jDoc = JsonDocument.Parse(responseString);
            string tokenString = jDoc.RootElement.GetProperty("token").GetString();
            ClaimsPrincipal claim = GetPrincipalFromToken(tokenString);
            return claim;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler tokenValidator = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.AuthenticationKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var parameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateLifetime = false
            };

            try
            {
                var principal = tokenValidator.ValidateToken(token, parameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    //_logger.LogError($"Token validation failed");
                    Debug.WriteLine($"Token validation failed");
                    return null;
                }

                return principal;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Token validation failed: {e.Message}");
                //_logger.LogError($"Token validation failed: {e.Message}");
                return null;
            }
        }


    }
}
