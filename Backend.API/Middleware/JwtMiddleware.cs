// using System.IdentityModel.Tokens.Jwt;
// using System.Text;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using Backend.API.Data;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Security.Claims;

// namespace Backend.API.Middleware
// {
//     public class JwtMiddleware
//     {
//         private readonly RequestDelegate _next;
//         private readonly IConfiguration _configuration;

//         public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
//         {
//             _next = next;
//             _configuration = configuration;
//         }

//         public async Task Invoke(HttpContext context, AppDbContext dbContext)
//         {
//             //  Allow CORS preflight requests
//             if (context.Request.Method == HttpMethods.Options)
//             {
//                 context.Response.StatusCode = StatusCodes.Status200OK;
//                 return;
//             }

//             // Skip JWT for auth endpoints (login/register)
//             if (context.Request.Path.StartsWithSegments("/api/auth"))
//             {
//                 await _next(context);
//                 return;
//             }

//             var token = context.Request.Headers["Authorization"]
//                 .FirstOrDefault()?
//                 .Split(" ")
//                 .Last();

//             if (!string.IsNullOrEmpty(token))
//             {
//                 AttachUserToContext(context, dbContext, token);
//             }

//             await _next(context);
//         }

//         private void AttachUserToContext(HttpContext context, AppDbContext dbContext, string token)
//         {
//             try
//             {
//                 var tokenHandler = new JwtSecurityTokenHandler();
//                 var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

//                 tokenHandler.ValidateToken(token, new TokenValidationParameters
//                 {
//                     ValidateIssuerSigningKey = true,
//                     IssuerSigningKey = new SymmetricSecurityKey(key),
//                     ValidateIssuer = false,
//                     ValidateAudience = false,
//                     ClockSkew = TimeSpan.Zero
//                 }, out SecurityToken validatedToken);

//                 var jwtToken = (JwtSecurityToken)validatedToken;
//                var userId = int.Parse(
//     jwtToken.Claims.First(x =>
//         x.Type == ClaimTypes.NameIdentifier
//     ).Value
// );


//                 context.Items["UserId"] = userId;
//             }
//             catch
//             {
//             }
//         }
//     }
// }
