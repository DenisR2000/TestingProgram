using System.ComponentModel.DataAnnotations;

namespace TesttingServer.Models.VewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Pasword is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
