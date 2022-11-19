using System.ComponentModel.DataAnnotations;

namespace BE_Company.Models.Request.Auth;

public class RegisterRequest
{
    [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "Yêu cầu nhập Email")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Mật khẩu lặp lại không chính xác")]
    public string ConfirmPassword { get; set; }
}