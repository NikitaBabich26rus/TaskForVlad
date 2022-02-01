using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> Login(string userName)
        {
            var account = await _db.FindByUserNameAsync(userName);
            if (account != null)
            {
                //TODO 1: Generate auth cookie for user 'userName' with external id
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, account.ExternalId),
                    new(ClaimTypes.Role, account.Role)
                };
                var id = new ClaimsIdentity(claims, "Cookie");
                await HttpContext.SignInAsync("Cookie", new ClaimsPrincipal(id));
                return Ok();
            }
            //TODO 2: return 404 if user not found
            return NotFound();
        }
    }
}