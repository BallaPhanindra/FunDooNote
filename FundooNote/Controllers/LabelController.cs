using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LabelController : Controller
    {
        private IConfiguration _config;
        private FundooNoteContext _funDoNoteContext;
        private ILabelBL _labelBL;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _memoryCache;
        private INoteBL _noteBL;
        public LabelController(ILabelBL labelBL, IConfiguration config, FundooNoteContext funDoNoteContext)
        {
            this._funDoNoteContext = funDoNoteContext;
            this._config = config;
            this._labelBL = labelBL;
        }
        [Authorize]
        [HttpPost("AddLabelName/{NoteId}/{labelName}")]
        public IActionResult AddLabel(int NoteId, string labelName)
        {
            var labelNote = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist so create a note to add label" });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            this._labelBL.AddLabel(UserID, NoteId, labelName);
            return this.Ok(new { success = true, status = 200, message = "Label added successfully" });
        }
        [Authorize]
        [HttpGet("GetLabels/{NoteId}")]
        public IActionResult GetLabels(int NoteId)
        {
            var labelNote = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = this._labelBL.GetLabelsByNoteId(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }
        [Authorize]
        [HttpGet("GetLabelByNoteIdwithJoin/{NoteId}")]
        public IActionResult GetLabelByNoteIdwithJoin(int NoteId)
        {
            var labelNote = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = this._labelBL.GetLabelByNoteIdwithJoin(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }
        [Authorize]
        [HttpGet("GetLabelByUserIdWithJoin")]
        public IActionResult GetLabelByUserIdWithJoin()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);


            var labels = this._labelBL.GetLabelByUserIdWithJoin(UserID);
            return this.Ok(new { success = true, status = 200, Labels = labels });
        }

        [Authorize]
        [HttpPut("UpdateLabel/{NoteId}/{newLabel}")]
        public async Task<IActionResult> UpdateLabel(int NoteId, string newLabel)
        {
            var labelNote = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            await this._labelBL.UpdateLabel(UserID, NoteId, newLabel);
            return this.Ok(new { success = true, status = 200, message = "Label Updated successfully" });
        }
        [Authorize]
        [HttpDelete("DeleteLabel/{NoteId}")]
        public async Task<IActionResult> DeleteLabel(int NoteId)
        {
            var labelNote = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
            if (labelNote == null)
            {
                return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist " });
            }
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            int UserID = Int32.Parse(userid.Value);

            await this._labelBL.DeleteLabel(UserID, NoteId);
            return this.Ok(new { success = true, status = 200, message = "Label Deleted successfully" });
        }
        [Authorize]
        [HttpGet("GetAllNoteByJoinUsingRedis")]
        public IActionResult GetAllNoteByJoinUsingRedis()
        {
            try
            {
                string CacheKey = "NoteList";
                string serializeNoteList;
                var noteList = new List<NoteResponseModel>();
                var redisNoteList = _cache.Get(CacheKey);

                if (redisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
                    noteList = JsonConvert.DeserializeObject<List<NoteResponseModel>>(serializeNoteList);
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                    int UserID = Int32.Parse(userid.Value);
                    noteList = this._noteBL.GetAllNotesUsingJoin(UserID);
                    serializeNoteList = JsonConvert.SerializeObject(noteList);
                    redisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    _cache.Set(CacheKey, redisNoteList, option);

                }
                return this.Ok(new { success = true, status = 200, noteList = noteList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}