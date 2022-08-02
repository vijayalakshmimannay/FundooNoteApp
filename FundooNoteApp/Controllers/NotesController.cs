using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    { 
            private readonly INotesBL iNotesBL;

            public NotesController(INotesBL iNotesBL)
            {
                this.iNotesBL = iNotesBL;
            }
            [Authorize]
            [HttpPost]
            [Route("Create")]
            public IActionResult CreateNote(NotesModel noteData)
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    var result = iNotesBL.AddNotes(noteData, userId);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Notes Created Successful", data = result });
                    }

                    return BadRequest(new { success = false, message = "Notes not Created" });
                }
                catch (Exception)
                {
                    // return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
                    throw;
                }
            }
        }

    }

