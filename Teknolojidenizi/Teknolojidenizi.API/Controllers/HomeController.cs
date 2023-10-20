
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Teknolojidenizi.BL;
using Teknolojidenizi.DAL.Entities;

namespace Divisima.API.Controllers
{
	[Route("api/[controller]"), Authorize]
	[ApiController]
	public class HomeController : ControllerBase
	{
		IRepository<Brand> repoBrand;
		IRepository<Admin> repoAdmin;
		public HomeController(IRepository<Brand> _repoBrand, IRepository<Admin> _repoAdmin)
		{
			repoBrand = _repoBrand;
			repoAdmin = _repoAdmin;
		}

		[HttpGet]
		public IEnumerable<Brand> GetBrands()
		{
			return repoBrand.GetAll().OrderBy(x => x.Name);
		}

		[HttpGet("{id}")]
		public Brand GetBrands(int id)
		{
			return repoBrand.GetBy(x => x.ID == id);
		}

		[HttpPost]
		public string Add(Brand model)
		{
			try
			{
				repoBrand.Add(model);
				return model.Name + " Markası başarı ile eklendi";
			}
			catch (Exception ex)
			{
				return "bir sorun oluştu, açıklama: " + ex.Message;
			}
		}

		[HttpDelete("{id}")]
		public string Delete(int id)
		{
			try
			{
				Brand brand = repoBrand.GetBy(x => x.ID == id);
				repoBrand.Delete(brand);
				return brand.Name + " markası başarı ile silindi";
			}
			catch (Exception ex)
			{
				return "silme işlemi esnasında bir sorun oluştu, açıklama: " + ex.Message;
			}
		}


		[HttpPut]
		public string Update(Brand model)
		{
			try
			{
				repoBrand.Update(model);
				return "Güncelleme işlemi başarılı";
			}
			catch (Exception ex)
			{
				return "Güncelleme işlemi esnasında bir sorun oluştu, açıklama:" + ex.Message;
			}
		}

		[AllowAnonymous, Route("/api/login"), HttpGet]
		public string Login(string username, string password)
		{
			string md5Password = GetMD5(password);
			Admin admin = repoAdmin.GetBy(x => x.UserName == username && x.Password == md5Password);
			if (admin != null) // eğer kayıt dönüyor ise
			{
				List<Claim> claims = new List<Claim>()
				{
					new Claim(ClaimTypes.PrimaryGroupSid, admin.ID.ToString()),
					new Claim(ClaimTypes.Name, admin.NameSurname)
				};

				string signInKey = "BuBenimOzelKeyim";
				SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(signInKey));
				SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

				JwtSecurityToken jwtSecurityToken = new(
					issuer: "http://localhost:5037/", // api sağlayıcı 
					audience: "n11", // kimliği kullanacak olan firma, uygulama adı
					claims: claims,
					expires: DateTime.Now.AddDays(10), // token geçerlilik süresi
					notBefore: DateTime.Now, // token geçerliliği ne zaman başlasın?
					signingCredentials: signingCredentials
					);

				string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
				return jwtToken;
			}
			else return "Başarısız...";
		}

		public static string GetMD5(string _text)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
				return BitConverter.ToString(hash).Replace("-", "");
			}
		}
	}
}
