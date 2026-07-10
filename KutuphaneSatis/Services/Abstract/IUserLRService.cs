using KutuphaneSatis.DTOs.Request.UserRequest;
using KutuphaneSatis.DTOs.Response.UserResponse;
using KutuphaneSatis.Enums;


namespace KutuphaneSatis.Services.Abstract
{
    public interface IUserLRService
    {

        public RegisterResultDto CreateUser(UserRegisterRequest userRegister);


        public LoginResult Check(UserLoginRequest userLoginRequest);

        public UserProfileResponse GetProfileByEmail(string userEmail);

        public UserProfileResponse GetUserById(int id);

        public void CreateCart(int id);


    }
}
