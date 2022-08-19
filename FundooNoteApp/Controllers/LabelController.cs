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
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        private readonly ILogger<LabelController> _logger;
        public LabelController(ILabelBL iLabelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext, ILogger<LabelController> logger)
        {
            this.iLabelBL = iLabelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
            _logger = logger;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(string Name, long noteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iLabelBL.CreateLabel(Name, noteID, userID);
                if (result)
                {
                    return Ok(new { success = true, message = "Label Created" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot create Label" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="LabelID">The label identifier.</param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("Get")]
        public IActionResult GetLabel(long LabelID)
        {
            try
            {
                var result = iLabelBL.GetLabel(LabelID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Got all Labels", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get Labels." });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="LabelName">Name of the label.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns></returns>

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLabel(string LabelName, long labelId)
        {
            try
            {
                var result = iLabelBL.UpdateLabel(LabelName, labelId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label updated." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot update label" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="LabelID">The label identifier.</param>
        /// <returns></returns>
        
        [HttpDelete]
        [Route("Delete")]
        public IActionResult RemoveLabel(long LabelID)
        {
            try
            {
                var result = iLabelBL.RemoveLabel(LabelID);
                if (result)
                {
                    return Ok(new { success = true, message = "Label is removed" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "cannot remove Label" });
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

            var cacheKey = "LabelList";
            string serializedLabelsList;
            var LabelsList = new List<LabelEntity>();
            var redisLabelsList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelsList != null)
            {
                serializedLabelsList = Encoding.UTF8.GetString(redisLabelsList);
                LabelsList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelsList);
            }
            else
            {
                LabelsList = fundooContext.LabelTable.ToList();
                serializedLabelsList = JsonConvert.SerializeObject(LabelsList);
                redisLabelsList = Encoding.UTF8.GetBytes(serializedLabelsList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelsList, options);
            }
            return Ok(LabelsList);
        }

    }
}

