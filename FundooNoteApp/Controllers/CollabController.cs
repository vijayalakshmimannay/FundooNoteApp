﻿using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public CollabController(ICollabBL icollabBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.icollabBL = icollabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
        [HttpDelete]
        [Route("Delete")]
        public IActionResult RemoveCollab(long CollabID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = icollabBL.RemoveCollab(CollabID, userID);
                if (result)
                {
                    return Ok(new { success = true, message = "Removed Collaborator.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot remove collaborator." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "userID").Value);

            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = fundooContext.CollabTable.ToList();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }

    }
}
