namespace PayRollProject.Entities.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel
    {
        
    }

    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "نام کاربری وارد نشده است.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "نام کاربری فقط می‌تواند شامل حروف، اعداد و زیرخط باشد.")] 
        [EmailAddress]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه عبور وارد نشده است.")]
        public string Password { get; set; }
    }
}