namespace PayRollProject.Entities.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserViewModel
    {
    }

    public class LoginViewModel
    {
        // [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "نام کاربری فقط می‌تواند شامل حروف، اعداد و زیرخط باشد.")] 
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری وارد نشده است.")]
        [RegularExpression(@"^[^\\/:*;\)\(]+$", ErrorMessage = "لطفا از کاراکتر های غیر مجاز استفاده نکنید.")]
        [EmailAddress]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "کلمه عبور وارد نشده است.")]
        public string Password { get; set; }
    }

    public class UserListDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Family { get; set; }

        public string MelliCode { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        // 1 = آقا
        // 2 = خانم
        public byte Gender { get; set; }

        public string GenderText { get; set; }

        // 1 = ادمین
        // 2 = کاربر
        public byte UserType { get; set; }

        public string UserTypeText { get; set; }

        // 1 = فعال
        // 2 = غیرفعال
        public byte UserFlag { get; set; }

        public string? UserFlagText { get; set; }
    }
}