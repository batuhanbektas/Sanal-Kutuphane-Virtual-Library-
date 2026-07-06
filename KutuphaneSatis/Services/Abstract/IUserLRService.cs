using KutuphaneSatis.DTOs.Request.UserRequest;
using KutuphaneSatis.Enums;


namespace KutuphaneSatis.Services.Abstract
{
    public interface IUserLRService
    {

        public RegisterEnums CreateUser(UserRegisterRequest userRegister);

        public LoginResult Check(UserLoginRequest userLoginRequest);


            
    }
}
