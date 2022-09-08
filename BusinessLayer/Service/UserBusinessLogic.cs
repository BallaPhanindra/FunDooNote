using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
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
        public bool ResetPassword(string email, ResetModel resetModel)
        {
            try
            {
                return this.userRL.ResetPassword(email, resetModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}