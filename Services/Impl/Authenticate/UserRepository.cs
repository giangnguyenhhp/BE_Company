using BE_Company.Models.Auth;
using BE_Company.Models.Request.Auth;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BE_Company.Services.Impl.Authenticate;

public class UserRepository : ControllerBase,IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var checkUser = await _userManager.FindByNameAsync(request.UserName);
        if (checkUser!=null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists" });
        }

        User user = new()
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName,
        };
        
        //Tạo user mới bằng method _userManager.CreatAsync(user,password)
        var result = await _userManager.CreateAsync(user, request.Password);

        //Kiểm tra xem trong Roles đã có role User hay chưa, nếu chưa có thì tạo role User.
        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new Role("User"));
        
        //Nếu đã có role User thì add role User cho người dùng mặc định.
        if(await _roleManager.RoleExistsAsync("User"))
            await _userManager.AddToRoleAsync(user, "User");

        return !result.Succeeded
            ? StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error", Message = "User creation failed! Please check user details and try again."
            })
            : Ok(new Response
            {
                Status = "Success", Message = "User creation successful!"
            });

    }
}