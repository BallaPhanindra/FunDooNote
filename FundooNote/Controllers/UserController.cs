using BusinessLayer.Interfaces;
using CommonLayer.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using System;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBusinessLogic _userBL;
        private IConfiguration _config;
        private readonly FundooNoteContext _fundooNoteContext;
        public UserController(IUserBusinessLogic userBL, IConfiguration config, FundooNoteContext fundooNoteContext )
        {
            this._userBL = userBL;
            this._config = config;
            this._fundooNoteContext = fundooNoteContext;
        }

        [HttpPost("RegisterUser")]

        public IActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this._userBL.RegisterUser(userPostModel);
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
                string token = this._userBL.LoginUser(loginModel);
                return this.Ok(new { Token = token, success = true, status = 200, message = $"login successful for {loginModel.Email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}