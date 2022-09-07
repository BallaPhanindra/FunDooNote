using BusinessLayer.Interfaces;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace funDoNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBusinessLogic userBL;
        private IConfiguration _config;

        public UserController(IUserBusinessLogic userBL, IConfiguration config)
        {
            this.userBL = userBL;
            this._config = config;
        }

        [HttpPost("RegisterUser")]

        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, status = 200, message = $"Registration successful for {userPostModel.Email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginModel loginModel)
        {
            try
            {
                string token = this.userBL.LoginUser(loginModel);
                return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {loginModel.Email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}