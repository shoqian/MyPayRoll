namespace PayRollProject.DataModel.ViewModel
{
    public enum UserTypeEnum
    {
         مدیر = 1,
         کاربر = 2
    }

    public enum GenderEnum
    {
        آقا = 1,
        خانم = 2
    }

    public enum UserFlagEnum
    {
        فعال = 1 ,
        غیرفعال = 2
    }

    public class ApplicationUserViewModel
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string Family { get; set; }
        //// 1 = user
        //// 2 = admin
        public UserTypeEnum UserTypeText { get; set; }

        //// 1 = male
        //// 2 = female
        public GenderEnum GenderText { get; set; }

        //// 1 = active
        //// 2 = deactivate
        public UserFlagEnum UserFlagText { get; set; }
    }
}