namespace BE_Company.Models.Auth;

public static class SystemPermissions
{
    public static readonly List<string> DefaultPermissions = new()
    {
        CreateCompany,
        ReadCompany,
        UpdateCompany,
        DeleteCompany,
        CreateDepartment,
        ReadDepartment,
        UpdateDepartment,
        DeleteDepartment,
        CreateEmployee,
        ReadEmployee,
        UpdateEmployee,
        DeleteEmployee,
        AdminAccess,
    };
    
    //Permissions
    public const string CreateCompany = "Company.Create";
    public const string ReadCompany = "Company.Read";
    public const string UpdateCompany = "Company.Update";
    public const string DeleteCompany = "Company.Delete";
    
    public const string CreateDepartment = "Department.Create";
    public const string ReadDepartment = "Department.Read";
    public const string UpdateDepartment = "Department.Update";
    public const string DeleteDepartment = "Department.Delete";
    
    public const string CreateEmployee = "Employee.Create";
    public const string ReadEmployee = "Employee.Read";
    public const string UpdateEmployee = "Employee.Update";
    public const string DeleteEmployee = "Employee.Delete";
    
    public const string AdminAccess = "AdminAccess";
}