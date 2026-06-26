# Hướng dẫn cài đặt và chạy dự án

Dự án này là một ứng dụng ASP.NET Core MVC sử dụng cơ sở dữ liệu MySQL. Dưới đây là các bước để thiết lập và chạy dự án trên máy của bạn.

## Yêu cầu hệ thống (Prerequisites)
1. **.NET SDK**: Phiên bản 10.0 trở lên. Bạn có thể tải tại [trang chủ của Microsoft](https://dotnet.microsoft.com/download).
2. **MySQL Server**: Cài đặt MySQL Server (và MySQL Workbench hoặc công cụ quản lý cơ sở dữ liệu tương đương như DBeaver, Navicat, phpMyAdmin).

## Bước 1: Thiết lập cơ sở dữ liệu (Database)
1. Mở công cụ quản lý MySQL của bạn (ví dụ: MySQL Workbench).
2. Kết nối vào MySQL Server bằng tài khoản `root` (hoặc tài khoản có quyền tạo database).
3. Chạy file script SQL `internship_db.sql` nằm trong thư mục `setup` này để tạo cơ sở dữ liệu và các bảng cần thiết.
   - Hoặc bạn có thể tự tạo một database có tên `internship_db` và chạy nội dung của file sql đó để tạo bảng và dữ liệu mẫu.

## Bước 2: Cấu hình chuỗi kết nối (Connection String)
1. Mở file `appsettings.json` nằm trong thư mục `src`.
2. Tìm đến phần `"ConnectionStrings"`.
3. Kiểm tra và chỉnh sửa lại cấu hình kết nối cho phù hợp với MySQL trên máy của bạn:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;User=root;Password=YOUR_MYSQL_PASSWORD;Database=internship_db"
   }
   ```
   *Lưu ý: Thay `YOUR_MYSQL_PASSWORD` bằng mật khẩu tài khoản `root` MySQL của bạn (mặc định trong code đang là `02081958N@m`).*

## Bước 3: Cài đặt các gói phụ thuộc (Dependencies)
1. Mở Terminal (Command Prompt hoặc PowerShell).
2. Di chuyển (cd) vào thư mục `src` của dự án:
   ```bash
   cd đường_dẫn_tới_dự_án/src
   ```
3. Chạy lệnh sau để khôi phục các gói (packages) cần thiết:
   ```bash
   dotnet restore
   ```

## Bước 4: Chạy dự án
1. Vẫn trong thư mục `src`, chạy lệnh sau để khởi động ứng dụng:
   ```bash
   dotnet run
   ```
2. Terminal sẽ hiển thị một đường dẫn (thường là `http://localhost:5000` hoặc `https://localhost:5001`). Mở đường dẫn đó trên trình duyệt web của bạn để xem và sử dụng ứng dụng.

## Bước 5: Đăng nhập
Sau khi truy cập vào ứng dụng, bạn có thể đăng nhập bằng tài khoản quản trị viên (Admin) để có toàn quyền quản lý hệ thống.
- **Tên đăng nhập (MSSV/Mã GV)**: `admin`
- **Mật khẩu**: `admin123`
