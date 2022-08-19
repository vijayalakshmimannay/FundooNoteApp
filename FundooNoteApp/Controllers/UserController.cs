using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {

        private readonly IUserBL iuserBL;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBL iuserBL,
            ILogger<UserController> logger)
        {
            this.iuserBL = iuserBL;
            _logger = logger;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("Register")]

        public IActionResult RegisterUser(UserRegistrationModel userRegistration)
        {
            try
            {
                var result = iuserBL.Registration(userRegistration);
                if(result != null)
                {
                    return Ok(new {success = true, message = "Registration Successful", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration UnSuccessful"});
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }
        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="userLoginModel">The user login model.</param>
        /// <returns></returns
        
        [HttpPost]
        [Route("Login")]

        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iuserBL.Login(userLoginModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("ForgetPassword")]

        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = iuserBL.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Email sent Successful"});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Email not Sent" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Resets the link.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns></returns> 
        
        [Authorize]
        [HttpPost]
        [Route("ResetLink")]

        public IActionResult ResetLink(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();

                var result = iuserBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Password Successful" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset Password not Sent" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

    }
}
