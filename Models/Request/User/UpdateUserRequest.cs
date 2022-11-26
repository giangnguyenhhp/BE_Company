namespace BE_Company.Models.Request.User;

public class UpdateUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> Roles { get; set; }
}