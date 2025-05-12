using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IdentityController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var client = _httpClientFactory.CreateClient();

            // Discovery endpoints from metadata
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:8081");
            if (disco.IsError)
            {
                return BadRequest(new { error = disco.Error, exception = disco.Exception?.Message });
            }

            // Request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "Admin",
                ClientSecret = "orders",
                Scope = "order"
            });

            if (tokenResponse.IsError)
            {
                return BadRequest(new { error = tokenResponse.Error, description = tokenResponse.ErrorDescription });
            }

            return Ok(new { access_token = tokenResponse.AccessToken, expires_in = tokenResponse.ExpiresIn });
        }
    }
}
