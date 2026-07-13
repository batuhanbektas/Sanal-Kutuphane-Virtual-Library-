using KutuphaneSatis.DTOs.Request.CartRequest;
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
        private readonly ICartService _cartService;
        private readonly ICartRepository _cartRepository;

        public UserLRService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, ICartService cartService, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _cartService = cartService;
            _cartRepository = cartRepository;
        }

        public RegisterResultDto CreateUser(UserRegisterRequest userRegister)
        {
            if (_userRepository.IsThereUserWithEmail(userRegister.Email))
            {
                return new RegisterResultDto { Status = RegisterEnums.EmailInUse};
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

                return new RegisterResultDto
                {
                    Status = RegisterEnums.Success,
                    UserId = user.Id,
                    Name = user.Name,
                };
            }

        }
        
        public void CreateCart(int id)
        {
            var user = _userRepository.GetByID(id);

            CreateCartRequest cartRequest = new CreateCartRequest()
            {
                UserId = user.Id
            };

            _cartService.CreateCart(cartRequest);
            
            var cartid = _cartRepository.ReturnCartId(id);
            
            user.CartId = cartid;

            _userRepository.Update(user);
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

        public UserProfileResponse GetUserById(int id) 
        {
            var user = _userRepository.GetByID(id);

            if (user == null)
            {
                return null;
            }
            return new UserProfileResponse
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                CartId = user.CartId

            };
            

        }




    }
}
