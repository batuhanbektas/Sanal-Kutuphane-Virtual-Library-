using KutuphaneSatis.DTOs.Request.UserRequest;
using KutuphaneSatis.Repositories.Abstract;
using KutuphaneSatis.Services.Abstract;
using KutuphaneSatis.Models.Concrete;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KutuphaneSatis.Services.Concrete
{
    public class UserLRService : IUserLRService
    {

        private readonly IUserRepository _userRepository;


        UserLRService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(UserRegisterRequest userRegister)
        {
           
                User user = new User()
                {
                    Name = userRegister.Name,
                    Surname = userRegister.Surname,
                    Email = userRegister.Email,
                    Password = userRegister.Password,


                };
        }
    }
}
