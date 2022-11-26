using BE_Company.Models.Auth;
using BE_Company.Models.Request.Auth;
using BE_Company.Models.Request.User;
using BE_Company.Services.Authenticate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_Company.Services.Impl.Authenticate;

public class UserRepository : ControllerBase, IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var checkUser = await _userManager.FindByNameAsync(request.UserName);
        if (checkUser != null)
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
        if (await _roleManager.RoleExistsAsync("User"))
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

    public async Task<IActionResult> RegisterAdmin(RegisterUserRequest request)
    {
        var checkUser = await _userManager.FindByNameAsync(request.UserName);
        if (checkUser != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });
        }

        User user = new()
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName,
        };

        //Tạo user mới bằng method _userManager.CreateAsync(user,password)
        var result = await _userManager.CreateAsync(user, request.Password);

        //Kiểm tra xem trong Roles đã có role Admin hay chưa, nếu chưa có thì tạo role Admin
        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new Role("Admin"));

        //Nếu đã có role Admin thì add role Admin vào cho user
        if (await _roleManager.RoleExistsAsync("Admin"))
            await _userManager.AddToRoleAsync(user, "Admin");

        //Kiểm tra xem tạo user mới có thành công không
        return !result.Succeeded
            ? StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "Create user failed!" })
            : Ok(new Response
                { Message = "User created successfully", Status = "Success" });
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response
            {
                Status = "Error",
                Message = "User not found!"
            });
        await _userManager.DeleteAsync(user);
        return Ok(new Response
        {
            Message = "User deleted successfully",
            Status = "Success"
        });
    }

    public async Task<IActionResult> RegisterForAdmin(RegisterUserRequest request)
    {
        var checkUser = await _userManager.FindByNameAsync(request.UserName);
        if (checkUser != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });
        }

        User user = new()
        {
            Id = Guid.NewGuid().ToString(),
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.UserName,
        };

        //Tạo user mới bằng method _userManager.CreateAsync(user,password)
        var result = await _userManager.CreateAsync(user, request.Password);

        //Add Role mình muốn vào user vừa tạo
        if (request.Roles != null)
        {
            await _userManager.AddToRolesAsync(user, request.Roles);
        }

        //Kiểm tra xem tạo user mới có thành công không
        return !result.Succeeded
            ? StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "Create user failed!" })
            : Ok(new Response
                { Message = "User created successfully", Status = "Success" });
    }

    public async Task<IActionResult> Update(string id, UpdateUserRequest request)
    {
        var checkUser = await _userManager.FindByIdAsync(id);
        if (checkUser == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User not existed" });
        }
        
        //Delete toàn bộ các role cũ của user
        var roles = await _userManager.GetRolesAsync(checkUser);
        await _userManager.RemoveFromRolesAsync(checkUser, roles);

        checkUser.UserName = request.UserName;
        checkUser.Email = request.Email;
        checkUser.PhoneNumber = request.PhoneNumber;

        if (request.Roles.Any())
        {
            await _userManager.AddToRolesAsync(checkUser, request.Roles);
        }

        await _userManager.UpdateAsync(checkUser);
        return Ok(new Response { Status = "Success", Message = "User updated successfully" });
    }

    public async Task<List<string>> GetRoleByUserId(string id)
    {
        var checkUser = await _userManager.FindByIdAsync(id);
        if (checkUser == null) throw new Exception("User not existed!!");
        
        //Lấy ra listRole<string> từ user bằng method GetRolesAsync
        var roles = await _userManager.GetRolesAsync(checkUser);
        return roles.ToList();
    }
}