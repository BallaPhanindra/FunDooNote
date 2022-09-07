using BusinessLayer.Interfaces;
using CommonLayer.User;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBusinessLogic
    {
        readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                return this.userRL.LoginUser(loginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userRL.RegisterUser(userPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                return userRL.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
