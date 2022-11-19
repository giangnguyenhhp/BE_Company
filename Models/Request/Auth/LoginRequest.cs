
using System.ComponentModel.DataAnnotations;

namespace BE_Company.Models.Request.Auth;

public class LoginRequest
{
    [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không chính xác")]
    public string ConfirmPassword { get; set; }
}