using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IKullaniciService _kullaniciService;

        public AuthController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        [AllowAnonymous]
        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] DtoLogin kullanici)
        {
            try
            {
                DtoKullaniciToken employee = _kullaniciService.GetKullaniciToken(kullanici.Eposta, kullanici.Sifre);

                if (employee != null)
                {
                    string token = GenerateToken(employee);

                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = false, // test için erişilebilir
                        Secure = false,   // localde false, canlıda true yap
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddHours(1)
                    };

                    TokenDomain(employee, cookieOptions);
                    Response.Cookies.Append("Token", token, cookieOptions);

                    // ✅ TOKEN'I DA DÖNDÜR
                    return Ok(new
                    {
                        message = "Giriş başarılı",
                        token = token,
                        kullanici = new
                        {
                            employee.Kullanici.Id,
                            employee.Kullanici.Eposta,
                            employee.Kullanici.Ad,
                            employee.Kullanici.Soyad,
                            employee.Kullanici.Rol
                        }
                    });
                }

                return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }


        private void TokenDomain(DtoKullaniciToken employee, CookieOptions cookieOptions)
        {
            if (Request.Headers.Origin.ToString().Contains("localhost"))
            {
                cookieOptions.Domain = "localhost";
            }
            else
            {
                cookieOptions.Domain = "localhost"; // Fallback domain
            }
        }

        private string GenerateToken(DtoKullaniciToken dto)
        {
            try
            {


                string securityKey = "bir_portal_bir_portala_gel_beraber_bir_portal_yapalim_demis";

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                var claims = new List<Claim>
                {
                    new Claim("Rol", dto.Kullanici.Rol.ToString()),
                    new Claim("Id", dto.Kullanici.Id.ToString()),
                    new Claim("Eposta", dto.Kullanici.Eposta ?? "E-mail bulunamadı"),
                    new Claim("Ad", dto.Kullanici.Ad ?? "Ad bulunamadı"),
                    new Claim("Soyad", dto.Kullanici.Soyad ?? "Soyad bulunamadı"),

                };

                var token = new JwtSecurityToken(
                        issuer: "mkalkan",
                        audience: "mkalkan",
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: signingCredentials
                        , claims: claims
                    );


                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }

        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                // Tarayıcıya set edilmiş olan JWT çerezini sil
                Response.Cookies.Delete("Token", new CookieOptions
                {
                    Secure = false, // local ortamda false, canlıda true yapılmalı
                    HttpOnly = false,
                    SameSite = SameSiteMode.None,
                    Domain = "localhost",
                    Path = "/"
                });

                return Ok(new { message = "Çıkış başarılı" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Çıkış sırasında hata oluştu", error = ex.Message });
            }
        }
    }
}
