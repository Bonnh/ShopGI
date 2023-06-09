﻿using System.ComponentModel.DataAnnotations;

namespace ShopGI.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Tên của bạn")]
		[Required(ErrorMessage = "Tên không được để trống")]
		public string Usernamee { get; set; }

        [Required(ErrorMessage ="Email không được để trống")]
        [EmailAddress(ErrorMessage ="Không hợp lệ!")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác thực không trùng khớp")]
        public string ConfirmPassword { get; set; }
        public string Coin { get; set; }
    }
}
