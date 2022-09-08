using BusinessLayer.Interface;
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
    public class LabelController : Controller
    {
        private IConfiguration _config;
        private FundooNoteContext _funDoNoteContext;
        private ILabelBL _labelBL;
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
    }
}