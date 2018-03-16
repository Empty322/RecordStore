using System.ComponentModel.DataAnnotations;

namespace ASP.Net_Core_turials.Models
{
	public class LoginViewModel
	{
		[Required]
		public string Login { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
