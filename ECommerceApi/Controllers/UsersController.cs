using ECommerceApi.Data;
using ECommerceApi.Dtos;
using ECommerceApi.Models;
using ECommerceApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ECommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            Logger.Log($"Kayıt başlatıldı: TC={userDto.Tc}, Email={userDto.Email}");
            Log.Information("Kayıt başlatıldı: TC={Tc}, Email={Email}", userDto.Tc, userDto.Email);

            if (userDto == null ||
                string.IsNullOrEmpty(userDto.Password) ||
                string.IsNullOrEmpty(userDto.Email) ||
                userDto.Tc == 0)
            {
                Logger.Log("Eksik bilgi ile kayıt denemesi");
                Log.Warning("Eksik bilgi ile kayıt denemesi");
                return BadRequest(new { message = "Eksik veya geçersiz kullanıcı bilgisi." });
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Tc == userDto.Tc);
            if (existingUser != null)
            {
                Logger.Log($"Kayıt başarısız: {userDto.Tc} zaten kayıtlı");
                Log.Error("Kayıt başarısız: {Tc} zaten kayıtlı", userDto.Tc);
                return Conflict(new { message = "Bu TC numarası zaten kayıtlı." });
            }

            if (userDto.Password != userDto.RePassword)
            {
                Logger.Log($"Kayıt başarısız: {userDto.Tc} Şifre tekrarı hatalı");
                Log.Error("Kayıt başarısız: {Tc} Şifre tekrarı hatalı", userDto.Tc);
                return Conflict(new { message = "Şifre tekrarı yanlış." });
            }

            var user = new User
            {
                Tc = userDto.Tc,
                Email = userDto.Email,
                Password = userDto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Logger.Log($"{user.Tc} Kayıt başarılı");
            Log.Information("Kayıt başarılı: TC={Tc}", user.Tc);

            return Ok(new { message = "Kayıt başarılı", userDto });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginUser)
        {
            Logger.Log("Giriş başlatıldı");
            Log.Information("Giriş başlatıldı: TC={Tc}", loginUser.Tc);

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Tc == loginUser.Tc);

            if (existingUser == null)
            {
                Logger.Log($"Giriş başarısız: {loginUser.Tc} bulunamadı");
                Log.Warning("Giriş başarısız: TC={Tc} bulunamadı", loginUser.Tc);
                return NotFound(new { message = "Kullanıcı Bulunamadı" });
            }

            if (existingUser.Tc == loginUser.Tc &&
               ((existingUser.Email != loginUser.Email) || (existingUser.Password != loginUser.Password)))
            {
                Logger.Log($"Giriş başarısız: TC {loginUser.Tc} için hatalı e-posta veya şifre.");
                Log.Warning("Giriş başarısız: TC={Tc} için hatalı e-posta veya şifre", loginUser.Tc);
                return BadRequest(new { message = "Kimlik numarası girilen kullanıcının E-postası veya Şifresi hatalı" });
            }

            Logger.Log($"Giriş başarılı: TC={existingUser.Tc}");
            Log.Information("Giriş başarılı: TC={Tc}", existingUser.Tc);

            return Ok(new
            {
                message = "Giriş Başarılı",
                tc = existingUser.Tc,
                email = existingUser.Email
            });
        }
    }
}
