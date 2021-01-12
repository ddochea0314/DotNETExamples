using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuth.Server.Controllers
{
    public class OAuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Authorize(
            string response_type, // authorization flow type
            string client_id, // client id
            string redirect_uri,
            string scope, 
            string state) // 동일한 클라이언트로 이동 함을 확인하기 위해 생성 된 임의의 문자열
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(
            string username,
            string redirectUri,
            string state)
        {
            const string code = "IT_MUST_REQUIRE";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            return Redirect($"{redirectUri}{query}");
        }


        public object Token(
            string grant_type,
            string code, 
            string redirect_uri,
            string client_id,
            string refresh_token)
        {
            // some mechanism for vaildating the code
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "subject"), // 토큰제목(sub)
                new Claim(JwtRegisteredClaimNames.UniqueName, "ddochea"),
                new Claim(JwtRegisteredClaimNames.Email, "ddochea@mail.com")
            };
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: grant_type == "refresh_token"
                ? DateTime.Now.AddMinutes(5)
                : DateTime.Now.AddMilliseconds(1),
                signingCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthTutorial",
                refresh_token = "RefreshTokenSampleValueSomething77",
            };

            //var responseJson = JsonConvert.SerializeObject(responseObject);
            //var responseBytes = Encoding.UTF8.GetBytes(responseJson);

            //await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
            //return Redirect(redirect_uri);
            return responseObject;
        }

        [Authorize]
        public IActionResult Vaildate()
        {
            if (HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {

                return Ok();
            }
            return BadRequest();
        }

    }
}
