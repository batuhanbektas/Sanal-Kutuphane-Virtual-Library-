using KutuphaneSatis.Enums;
using KutuphaneSatis.Models.Concrete;

public class LoginResult
{
    public LoginEnums Status { get; set; }
    public User UserInfo { get; set; } // Veritabanından çektiğin kullanıcı nesnesi
}