using KutuphaneSatis.DTOs.Request.UserRequest;
using KutuphaneSatis.DTOs.Response.UserResponse;
using KutuphaneSatis.Enums;
using KutuphaneSatis.Models.Concrete;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Services.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KutuphaneSatis.Services.Concrete
{
    public class UserLRService : IUserLRService
    {

        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLRService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public RegisterEnums CreateUser(UserRegisterRequest userRegister)
        {
            if (_userRepository.IsThereUserWithEmail(userRegister.Email))
            {
                return RegisterEnums.EmailInUse;
            }
            else
            {
                User user = new User()
                {
                    Name = userRegister.Name,
                    Surname = userRegister.Surname,
                    Email = userRegister.Email,
                    Password = userRegister.Password,
                };
                _userRepository.Create(user);
                return RegisterEnums.Success;

            }

        }

        
        public LoginResult Check(UserLoginRequest request)
        {
            // 1. Veritabanında kullanıcıyı e-posta ile ara
            var user = _userRepository.GetByEmail(request.Email);


            // 2. Kullanıcı yoksa UserInfo'yu null gönder
            if (user == null)
            {
                return new LoginResult { Status = LoginEnums.UserNotFound, UserInfo = null };
            }

            // 3. Şifre yanlışsa
            if (user.Password != request.Password)
            {
                return new LoginResult { Status = LoginEnums.WrongPassword, UserInfo = null };
            }

            // 4. Her şey doğruysa (Başarılı) -> UserInfo'ya bulduğumuz kullanıcıyı koyuyoruz!
            return new LoginResult { Status = LoginEnums.Success, UserInfo = user };
        }

        public UserProfileResponse GetProfileByEmail(string userEmail)
        {
            var user = _userRepository.GetByEmail(userEmail);


            if (user == null)
            {

                return null;

            }
            return new UserProfileResponse
            {

                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname


            };
        
        
        }





    }
}
