using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusinessLogic
    {
        void RegisterUser(UserPostModel userPostModel);
    }
}