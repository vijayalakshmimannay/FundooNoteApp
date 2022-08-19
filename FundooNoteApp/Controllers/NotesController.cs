using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotesController : ControllerBase
    {
        private readonly INotesBL iNotesBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INotesBL iNotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext, ILogger<NotesController> logger)
        {
            this.iNotesBL = iNotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            _logger = logger;

        }
        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns></returns>
        
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
        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <returns></returns>
        
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
        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        
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

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteModel">The note model.</param>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        
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

        /// <summary>
        /// Pins to dashboard.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        
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
        /// <summary>
        /// Archives the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.Archive(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Archived successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note UnArchived successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Trashes the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <returns></returns>
        
        [HttpPut]
        [Route("Trash")]
        public IActionResult Trash(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.Trash(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Notes Trashed successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Notes UnTrashed successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Colours the specified notes identifier.
        /// </summary>
        /// <param name="notesId">The notes identifier.</param>
        /// <param name="colour">The colour.</param>
        /// <returns></returns>
        
        [HttpPut]
        [Route("Colour")]
        public IActionResult Colour(long notesId, string colour)
        {
            try
            {
                var result = iNotesBL.Colour(notesId, colour);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Colour Changed", data = result });
                }
                else
                {
                    return NotFound(new { success = false, message = "Colour change Failed" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("Add Image")]
        public IActionResult AddImage(long noteId, IFormFile image)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.AddImage(noteId, userID, image);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Image Uploaded Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessful", Data = result });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets all customers using redis cache.
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "userID").Value);

            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }

    }


}

