using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using System;
using System.Linq;

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
    }
}