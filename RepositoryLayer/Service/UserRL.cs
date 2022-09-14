using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration config;
        private readonly FundooContext fundooContext;
        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
           
          
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
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.Password = userRegistrationModel.Password;
               // userEntity.Password = EncryptPassword(userRegistrationModel.Password);

                fundooContext.UserEntities.Add(userEntity);
                int result = fundooContext.SaveChanges();

                if(result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
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
                var LoginResult = this.fundooContext.UserEntities.Where(user => user.Email == userLoginModel.Email).FirstOrDefault();
                //if(LoginResult != null && Decryption(LoginResult.Password) == userLoginModel.Password)
                  if (LoginResult != null && LoginResult.Password == userLoginModel.Password)
                    {
                    var token = GenerateSecurityToken(LoginResult.Email, LoginResult.UserId);
                   // userLoginModel.UserName = LoginResult.FirstName;
                   // userLoginModel.Email = LoginResult.Email;
                  //  userLoginModel.Password = LoginResult.Password;

                    return token;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Generates the security token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        
        public string GenerateSecurityToken(string email, long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("JWT:key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

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
                var emailCheck = fundooContext.UserEntities.FirstOrDefault(x => x.Email == Email);
                if(emailCheck != null)
                {
                    var Token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSMQmodel mSMQmodel = new MSMQmodel();
                    mSMQmodel.sendData2Queue(Token);
                    return Token.ToString();
                }
                else
                {
                    return null;
                }

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
                if(password.Equals(confirmPassword))
                {
                    var emailCheck = fundooContext.UserEntities.FirstOrDefault(x => x.Email == email);
                    emailCheck.Password = password;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
              
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        
        public string EncryptPassword(string password)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        /// <summary>
        /// Decryptions the specified encrypted pass.
        /// </summary>
        /// <param name="encryptedPass">The encrypted pass.</param>
        /// <returns></returns>
        
        public static string Decryption(string encryptedPass)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(encryptedPass))
            {
                return "";
            }
            var encodeBytes = Convert.FromBase64String(encryptedPass);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}

