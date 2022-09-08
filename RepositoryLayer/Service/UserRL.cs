using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        readonly FundooNoteContext _funDoNoteContext;
        private IConfiguration _config;
        public UserRL(FundooNoteContext funDoNoteContext, IConfiguration config)
        {
            this._funDoNoteContext = funDoNoteContext;
            this._config = config;
        }

        public string LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = _funDoNoteContext.users.Where(x => x.Email == loginModel.Email && x.Password == loginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                return GenerateJwtToken(user.Email,user.UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private string GenerateJwtToken(string email, int userId)
        {

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("Email", email),
                        new Claim("UserId", userId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials =
                    new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
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
                User user = new User();
                user.FirstName = userPostModel.FirstName;
                user.LastName = userPostModel.LastName;
                user.Email = userPostModel.Email;
                user.Password = userPostModel.Password;
                user.ConfirmPassword = userPostModel.ConfirmPassword;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                _funDoNoteContext.users.Add(user);
                _funDoNoteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateToken(string email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email)
                    }),
                    Expires
                    = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                         new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
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
                var user = _funDoNoteContext.users.Where(x => x.Email == email).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                MessageQueue funDoNoteQ = new MessageQueue();
                //Setting the QueuPath where we want to store the messages.
                funDoNoteQ.Path = @".\private$\funDoNote";
                if (MessageQueue.Exists(funDoNoteQ.Path))
                {
                    funDoNoteQ = new MessageQueue(@".\private$\funDoNote");

                    //Exists
                }
                else
                {
                    // Creates the new queue named "funDoNote"
                    MessageQueue.Create(funDoNoteQ.Path);

                }
                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GenerateJwtToken(email, user.UserId);
                message.Label = "Forget Password Email";
                funDoNoteQ.Send(message);
                Message msg = funDoNoteQ.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(email, message.Body.ToString(), email);
                funDoNoteQ.ReceiveCompleted += new ReceiveCompletedEventHandler(ReceiveCompleted);
                funDoNoteQ.BeginReceive();
                funDoNoteQ.Close();
                return true;

            }
            catch (MessageQueueException ex)

            {
                throw ex;
            }
        }
        private void ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==

                    MessageQueueErrorCode.AccessDenied)

                {

                    Console.WriteLine("Access is denied. " +

                        "Queue might be a system queue.");

                }
            }
        }

        public bool ResetPassword(string email, ResetModel resetModel)
        {
            try
            {
                var user = _funDoNoteContext.users.Where(x => x.Email == email).FirstOrDefault();
                if (resetModel.NewPassword != resetModel.ConfirmNewPassword)
                {
                    return false;
                }
                user.Password = resetModel.NewPassword;
                _funDoNoteContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}