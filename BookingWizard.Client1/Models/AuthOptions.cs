using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookingWizard.Client.Models
{
	public class AuthOptions
		{
			public const string ISSUER = "https://localhost:7037"; 
			public const string AUDIENCE = "BookingWizard.Client";
			const string KEY = "mysupersecretkey123";
			public const int LIFETIME = 3; // время жизни токена - 1 минута
			public static SymmetricSecurityKey GetSymmetricSecurityKey()
			{
				return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
			}
		}
}
