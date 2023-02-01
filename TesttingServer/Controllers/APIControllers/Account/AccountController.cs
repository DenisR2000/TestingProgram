using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Threading.Tasks;
using TesttingServer.JWTConfig;
using TesttingServer.Models;
using TesttingServer.Models.VewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace TesttingServer.Controllers.APIControllers.Account
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IdentityContext db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IdentityContext db)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
                    if (result.Succeeded)
                    {
                        var identity = await GetIdentity(user.Email, model.Password);
                        if (identity == null)
                        {
                            return BadRequest("Invalid login or password");
                        }
                        var jwt = new JwtSecurityToken(
                            issuer: AuthOptions.ISSURER,
                            audience: AuthOptions.AUDIENCE,
                            claims: identity.Claims,
                            expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                        );
                        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                        return Json(new
                        {
                            access_token = encodedJwt,
                            userId = user.Id,
                            roles = await userManager.GetRolesAsync(user)
                        });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login or password");
                        return BadRequest(ModelState);
                    }
                }
                return BadRequest(new { error = "User not found" });
            }
            return BadRequest(model);
        }
        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            var result = await signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType,user.Email),
                };
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else
            {
                ModelState.AddModelError("", "Invalid login or password");
            }
            return null;
        }
    }
}
