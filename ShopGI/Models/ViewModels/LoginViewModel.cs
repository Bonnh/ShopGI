using System.ComponentModel.DataAnnotations;

namespace ShopGI.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
