## 📂 Hướng dẫn Cấu trúc Thư mục Frontend (StoreManagement.Client)

Để đảm bảo dự án gọn gàng và dễ bảo trì, vui lòng tuân thủ quy tắc đặt file vào đúng thư mục sau:

| Thư mục / File | Mô tả & Quy tắc sử dụng |
| :--- | :--- |
| **`Pages/`** | **Nơi chứa các trang giao diện chính.**<br>• Mỗi file `.razor` ở đây tương ứng với một màn hình (VD: `BookList.razor`, `Login.razor`).<br>• Bắt buộc phải có dòng `@page "/duong-dan"` ở đầu file. |
| **`Layout/`** | **Nơi chứa khung giao diện chung.**<br>• `MainLayout.razor`: Khung sườn bao quanh web (Header, Footer).<br>• `NavMenu.razor`: Thanh menu bên trái. <br>• Nếu muốn sửa menu thì vào đây, đừng tìm trong Pages. |
| **`Models/`** | **Nơi chứa các Class mô tả dữ liệu (DTO).**<br>• Chứa các file `.cs` như `Book.cs`, `Category.cs`.<br>• **Lưu ý:** Các class này chỉ chứa thuộc tính (Property) để hứng dữ liệu từ API. Không được chứa logic xử lý phức tạp hay thư viện của Backend. |
| **`wwwroot/`** | **Kho chứa tài nguyên tĩnh.**<br>• `css/`: File định dạng giao diện (Bootstrap, app.css).<br>• `images/`: Chứa logo, banner, icon.<br>• `index.html`: File HTML gốc chạy đầu tiên khi vào web. |
| **`_Imports.razor`** | **Khai báo thư viện dùng chung.**<br>• Nếu bạn thấy mình phải gõ `using ...` giống nhau ở quá nhiều file, hãy thêm dòng đó vào đây 1 lần để áp dụng cho toàn bộ dự án. |
| **`Program.cs`** | **Bộ não khởi động.**<br>• Nơi cấu hình kết nối tới Backend (BaseAddress).<br>• Nơi đăng ký các Service (Dependency Injection). |
| **`App.razor`** | **Bộ định tuyến (Router).**<br>• Điều phối người dùng đến đúng trang (Page) dựa trên đường dẫn URL. |
| **`bin/` & `obj/`** | **⚠️ KHÔNG CHẠM VÀO.**<br>• Thư mục hệ thống tự sinh ra khi chạy code. Có thể xóa nếu gặp lỗi lạ, nhưng không được sửa file bên trong. |

---
