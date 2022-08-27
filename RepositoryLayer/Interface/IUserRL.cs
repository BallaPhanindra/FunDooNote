using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void Register(UserPostModel userPostModel);
   
    }
}
