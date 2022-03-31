using App.Application.Dtos;
using App.Application.Helpers;
using App.Application.Interfaces;
using App.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace App.Application.Services
{
    public class UserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContext;

        public UserService(AppDbContext db, IMapper mapper, IEmailService emailService, IHttpContextAccessor httpContext)
        {
            _db = db;
            this.mapper = mapper;
            this.emailService = emailService;
            this.httpContext = httpContext;
        }

        public async Task<UserDto> Login(UserDto user)
        {
            var loginVerify = _db.Users.FirstOrDefault(u => u.UserName == user.UserName ||
                u.Email == user.UserName ||
                u.Phone == user.UserName &&
                u.Password == user.Password);

            if (loginVerify != null)
            {
                var claims = new List<Claim>()
                {
                   new Claim(ClaimTypes.NameIdentifier,loginVerify.Id.ToString()),
                   new Claim(ClaimTypes.Name, loginVerify.UserName),
                   new Claim(ClaimTypes.GivenName, loginVerify.Name ?? " "),
                   new Claim(ClaimTypes.Email, loginVerify.Email),
                };

                if (!string.IsNullOrEmpty(loginVerify.Role))
                {
                    var rolesArr = loginVerify.Role.Split(',');
                    foreach (var role in rolesArr)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
                    }
                }

                // ClaimsIdentity (Kimlik)
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Principle (Cüzdan)
                var claimsPriciple = new ClaimsPrincipal(claimsIdentity);

                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = user.RememberMe,
                    ExpiresUtc = DateTime.Now.AddMinutes(180),
                };

                await httpContext.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPriciple,
                    authProperties
                    );
            }

            return user;
        }

        public bool LogOut()
        {
            httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return true;
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            user.IsActive = false;
            var userRegister = mapper.Map<User>(user);

            userRegister.Name = user.UserName;
            userRegister.CreatedAt = DateTime.Now;
            userRegister.UpdatedAt = DateTime.Now;
            userRegister.Role = "User";
            userRegister.ProfileImagePath = "https://cdn.discordapp.com/attachments/916029512884563999/949102276654538792/user.png";
            userRegister.ProfileImageThumb = "https://pbs.twimg.com/profile_banners/4612473879/1543594583/1500x500";
            userRegister.ActivationKey = StringHelper.Base64Encode(Random.Shared.Next(100, 999).ToString());

            _db.Users.Add(userRegister);
            _db.SaveChanges();

            var body = @$"
                <html>
                    <head>
                    <style>h1 {{ color: red }}</style>
                    </head>
                    <body>
                        <h1>Twitter | Clone</h1>
                        <h2>Üyelik Aktivasyonu</h2>
                        <p>Üyeliğinizi aktif etmek için aşağıdaki bağlantıya tıklayınız.</p>
                        <a href=""https://localhost:7269/Auth/Login?email={user.Email}&activationKey={userRegister.ActivationKey}"">Aktive Et</a>
                    </body>
                </html>
            ";

            this.emailService.Send(user.Email, "Twitter Clone: Üyelik Aktivasyonu", body);

            return user;
        }

        public async Task<UserDto> ForgotPassword(UserDto user)
        {
            var user2 = _db.Users.FirstOrDefault(x => x.Email == user.Email || x.UserName == user.UserName);

            if (user2 != null)
            {
                user2.FgPwActivationKey = StringHelper.Base64Encode(Random.Shared.Next(200, 1999).ToString());

                _db.Users.Update(user2);
                await _db.SaveChangesAsync();

                var body = @$"
                    <html>
                        <head>
                        <style>h1 {{ color: red }}</style>
                        </head>
                        <body>
                            <h1>Twitter | Clone</h1>
                            <h2>Üyelik Şifre Sıfırlama</h2>
                            <p>Üyeliğinizin şifresini sıfırlamak için aşağıdaki bağlantıya tıklayınız.</p>
                            <a href=""https://localhost:7269/Auth/Forgot?email={user.Email}&activationKey={user2.FgPwActivationKey}"">Şifremi Sıfırla</a>
                        </body>
                    </html>
                ";

                this.emailService.Send(user.Email, "Twitter Clone: Üyelik Şifre Sıfırlama", body);

                return user;
            }

            return user;
        }

        public bool ValidateAndActivate(string email, string activationKey)
        {
            var user = _db.Users.FirstOrDefault(e => e.Email == email && e.ActivationKey == activationKey);

            if (user != null)
            {
                user.IsActive = true;
                _db.SaveChanges();
            }
            return user != null;
        }

        public async Task<User> Profile(string name)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<User> Navbar()
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserName == httpContext.HttpContext.User.Identity.Name);
        }
    }
}