using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL 
    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }
        /// <summary>
        /// Registrations the specified user registration model.
        /// </summary>
        /// <param name="userRegistrationModel">The user registration model.</param>
        /// <returns></returns>
        
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// Logins the specified user login model.
        /// </summary>
        /// <param name="userLoginModel">The user login model.</param>
        /// <returns></returns>
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.Login(userLoginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
        public string ForgetPassword(string Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Resets the link.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns>
        public bool ResetLink(string email, string password, string confirmPassword)
        {
            try
            {
                return iuserRL.ResetLink(email, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
