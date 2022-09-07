using BusinessLayer.Interfaces;
using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using System;
using System.Linq;

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
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                bool isTrue = this._userBL.ForgotPassword(email);
                if (isTrue)
                {
                    return this.Ok(new { success = true, status = 200, message = $"Reset link sent to {email}" });
                }
                return this.Ok(new { success = false, status = 404, message = $"wrong {email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetModel resetModel)
        {
            try
            {
                if (resetModel.NewPassword != resetModel.ConfirmNewPassword)
                {
                    return this.BadRequest(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
                //authorization match email from token
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var result = _fundooNoteContext.users.Where(u => u.UserId == UserID).FirstOrDefault();
                string Email = result.Email.ToString();
                bool res = this._userBL.ResetPassword(Email, resetModel);
                if (res == false)
                {
                    return this.BadRequest(new { success = false, message = $"Password not updated" });
                }
                return this.Ok(new { success = true, status = 200, message = "Password Changed Sucessfully" });
                ////Authorization by email
                //var identity = User.Identity as ClaimsIdentity;
                //if (identity != null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                //    var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                //    this.userBL.ResetPassword(email, resetModel);
                //    return this.Ok(new { success = true, message = "Password Changed Sucessfully", email = $"{email}" });
                //}
            }
            catch (MessageQueueException ex)
            {
                throw ex;
            }
        }
    }
}