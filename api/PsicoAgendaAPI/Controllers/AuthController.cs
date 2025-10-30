using Infrastructure.Auth.Model;
using Infrastructure.Auth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PsicoAgendaAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<UserApplication> _user;
        private readonly SignInManager<UserApplication> _signIn;
        private readonly IJwtTokenService _tokens;

        public AuthController(UserManager<UserApplication> user, SignInManager<UserApplication> signIn, IJwtTokenService tokens)
        {
            _user = user;
            _signIn = signIn;
            _tokens = tokens;
        }

        public record RegisterRequest(string UserName, string Email, string Password);
        public record LoginRequest(string UserNameOrEmail, string Password);
        public record TokenResponse(string AccessToken, string RefreshToken);
        public record RefreshRequest(string RefreshToken);

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest req)
        {
            var user = new UserApplication() { UserName = req.UserName, Email = req.Email, EmailConfirmed = true };
            var result = await _user.CreateAsync(user, req.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _user.AddToRoleAsync(user, "user");
            var (access, refresh) = await _tokens.IssueTokensAsync(user);
            return Ok(new TokenResponse(access, refresh.Token));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = await _user.FindByNameAsync(req.UserNameOrEmail)
                       ?? await _user.FindByEmailAsync(req.UserNameOrEmail);
            if (user is null) return Unauthorized();

            var check = await _signIn.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
            if (!check.Succeeded) return Unauthorized();

            var (access, refresh) = await _tokens.IssueTokensAsync(user);
            return Ok(new TokenResponse(access, refresh.Token));
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh(RefreshRequest req)
        {
            var (access, refresh) = await _tokens.RotateRefreshAsync(req.RefreshToken);
            return Ok(new TokenResponse(access, refresh.Token));
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
            => Ok(new
            {
                User = User.Identity!.Name,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
    }
}

