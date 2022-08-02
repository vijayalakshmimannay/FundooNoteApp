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
           [HttpPost]
           [Route("GetNotes")]
            public IActionResult GetNotes()
            {
                try
                {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.GetNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Received ", data = result });
                }
                return BadRequest(new { success = false, message = "Notes not Received" });
                }
                catch (System.Exception)
                {

                throw;
                }
            }
            [HttpDelete]
            [Route("DeleteNote")]
            public IActionResult DeleteNotes(long NoteID)
            { 
                try
                {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.DeleteNotes(userId, NoteID);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Note Deleted." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot delete nNte." });
                }
            }
                catch (System.Exception)
                {
                   throw;
                }
            }

            [HttpPut]
            [Route("UpdateNote")]
            public IActionResult UpdateNote(NotesModel noteModel, long NoteID)
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    var result = iNotesBL.UpdateNote(noteModel, NoteID, userId);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Note Updated Successfully.", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Cannot update note." });
                     }
                }
                catch (System.Exception)
                {
                   throw;
                }
            }

            [HttpPut]
            [Route("Pin")]
            public IActionResult pinToDashboard(long NoteID)
            {
                try
                {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.PinToDashboard(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note Unpinned successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
    }
}

