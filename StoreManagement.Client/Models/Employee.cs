using System;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Client.Models
{
    public class Employee : BaseEntity
    {
        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime? DateOfBirth { get; set; } = DateTime.Today.AddYears(-18);

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Chức vụ là bắt buộc")]
        public string Position { get; set; } = string.Empty;

        public EmployeeAccount Account { get; set; } = new EmployeeAccount();
    }

    public class EmployeeAccount
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; } = string.Empty;

        public EmployeeRole Role { get; set; } = EmployeeRole.STAFF;

        public bool IsActive { get; set; } = true;
    }

    public enum EmployeeRole
    {
        ADMIN,
        STAFF
    }
}
