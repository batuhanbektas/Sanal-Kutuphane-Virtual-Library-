using KutuphaneSatis.Enums;

namespace KutuphaneSatis.DTOs.Response.UserResponse // Kendi namespace'ine göre ayarla
{
    public class RegisterResultDto
    {
        public RegisterEnums Status { get; set; }
        public int UserId { get; set; } // Eğer ID tipin Guid ise Guid yapmalısın\

        public string Name { get; set; }
    }
}