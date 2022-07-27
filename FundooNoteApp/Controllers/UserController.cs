using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBL iuserBL;

        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }

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
            catch (System.Exception)
            {

                throw;
            }

        }
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

    }
}
