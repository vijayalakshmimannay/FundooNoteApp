using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;
        public CollabController(ICollabBL icollabBL)
        {
            this.icollabBL = icollabBL;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateCollab(long NoteID, string CollabEMail)
        {
            try
            {
                var result = icollabBL.CreateCollab(NoteID, CollabEMail);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collaborator Created successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot create collaborator." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetCollab()
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = icollabBL.GetCollab(userID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Got all collaborator notes.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get collaborator." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
