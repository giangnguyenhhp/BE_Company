## Controller 
- Là một lớp kế thừa từ lớp Controller : Microsoft.AspNet.Mvc.Controller
- Action trong Controller là một method Public (Không được static)
- Action trả về bất kì kiểu dữ liệu nào, thường là IActionResult
- Các dịch vụ inject vào Controller qua hàm tạo
## View
- Là file .cshtml
- View cho Action lưu tại : /View/ControllerName/Action.cshtml
- Thêm thư mục lưu trữ cho View
{0} -> tên Action
{1} -> Tên Controller
{2} -> Tên Area
options.ViewLocationFormats.Add("/MyView/{1}/{0}.cshtml" + RazorViewEngine.ViewExtension)
## Truyền dữ liệu sáng View
- Model
- ViewData
- TempData
- ViewBag