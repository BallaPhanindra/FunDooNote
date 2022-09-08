using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        private INoteBL _noteBL;
        private IConfiguration _config;
        private FundooNoteContext _funDoNoteContext;
        public NoteController(INoteBL noteBL, IConfiguration config, FundooNoteContext funDoNoteContext)
        {
            this._funDoNoteContext = funDoNoteContext;
            this._config = config;
            this._noteBL = noteBL;
        }
        [Authorize]
        [HttpPost("AddNote")]
        public IActionResult AddNote(NoteModel noteModel)
        {
            try
            {
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                this._noteBL.AddNote(noteModel, UserID);
                return this.Ok(new { success = true, status = 200, message = "Note Added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateNote/{NoteId}")]
        public IActionResult UpdateNote(UpdateNoteModel updateNoteModel, int NoteId)
        {
            try
            {
                var note = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Please provide correct note" });
                }
                this._noteBL.UpdateNote(updateNoteModel, UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteId}")]
        public IActionResult DeleteNote(int NoteId)
        {
            try
            {
                var note = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Please provide correct note" });
                }
                this._noteBL.DeleteNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetNote/{NoteId}")]
        public IActionResult GetNote(int NoteId)
        {
            try
            {
                var note = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Note not exist" });
                }
                Note noteOne = new Note();
                noteOne = this._noteBL.GetNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, noteList = noteOne });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllNote")]
        public IActionResult GetAllNote()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<Note> note = new List<Note>();
                note = this._noteBL.GetAllNotes(UserID);
                return this.Ok(new { success = true, status = 200, noteList = note });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNoteJoin")]
        public IActionResult GetAllNotesUsingJoin()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<NoteResponseModel> note = new List<NoteResponseModel>();
                note = this._noteBL.GetAllNotesUsingJoin(UserID);
                return this.Ok(new { success = true, status = 200, noteList = note });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("ArchieveNote/{NoteId}")]
        public async Task<IActionResult> ArchieveNote(int NoteId)
        {
            try
            {
                var note = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);


                var archieve = await this._noteBL.ArchieveNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note archieved successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("PinNote/{NoteId}")]
        public async Task<IActionResult> PinNote(int NoteId)
        {
            try
            {
                var note = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);


                var archieve = await this._noteBL.PinNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note Pinned successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("TrashNote/{NoteId}")]
        public async Task<IActionResult> TrashNote(int NoteId)
        {
            try
            {
                var note = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                await this._noteBL.TrashNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note sended to Trash successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ReminderNote/{NoteId}")]
        public async Task<IActionResult> ReminderNote(int NoteId, NoteReminderModel reminder)
        {
            try
            {
                var note = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                var rem = Convert.ToDateTime(reminder.Reminder);
                await this._noteBL.ReminderNote(UserID, NoteId, rem);
                return this.Ok(new { success = true, status = 200, message = "Note  reminder added successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteReminderNote/{NoteId}")]
        public async Task<IActionResult> DeleteReminderNote(int NoteId)
        {
            try
            {
                var note = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                await this._noteBL.DeleteReminderNote(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, message = "Note reminder Deleted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateColor/{NoteId}/{color}")]
        public async Task<IActionResult> UpdateColor(int NoteId, string color)
        {
            try
            {
                var note = await _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefaultAsync();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, status = 400, message = "Note doesn't exist" });
                }
                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);
                await this._noteBL.UpdateColor(UserID, NoteId, color);
                return this.Ok(new { success = true, status = 200, message = "Colour updated successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllColour")]
        public IActionResult GetAllColour()
        {
            try
            {

                //Authorization match userId
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserID = Int32.Parse(userid.Value);

                List<GetColorModel> note = new List<GetColorModel>();
                note = this._noteBL.GetAllColour(UserID);
                return this.Ok(new { success = true, status = 200, noteList = note });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}