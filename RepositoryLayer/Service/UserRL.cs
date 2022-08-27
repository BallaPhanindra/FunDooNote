using CommonLayer.User;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Service.Entities;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly FundooNoteContext funDoNoteContext;
        public UserRL(FundooNoteContext funDoNoteContext)
        {
            this.funDoNoteContext = funDoNoteContext;
        }
        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.FirstName = userPostModel.FirstName;
                user.LastName = userPostModel.LastName;
                user.Email = userPostModel.Email;
                user.Password = userPostModel.Password;
                user.ConfirmPassword = userPostModel.ConfirmPassword;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                funDoNoteContext.users.Add(user);
                funDoNoteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}