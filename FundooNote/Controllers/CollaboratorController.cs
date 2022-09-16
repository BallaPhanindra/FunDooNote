using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace funDoNote.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollaboratorController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly FundooNoteContext _funDoNoteContext;
        private readonly ICollaboratorBL _collaboratorBL;
        public CollaboratorController(IConfiguration config, FundooNoteContext funDoNoteContext, ICollaboratorBL collaboratorBL)
        {
            _config = config;
            _funDoNoteContext = funDoNoteContext;
            _collaboratorBL = collaboratorBL;
        }
        [Authorize]
        [HttpPost("AddCollab/{NoteId}/{email}")]
        public async Task<IActionResult> AddCollab(int NoteId, string email)
        {
            try
            {
                var note = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (note == null)
                {
                    return BadRequest(new { success = false, message = "Note not found" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                await _collaboratorBL.AddCollaborator(UserID, NoteId, email);
                return this.Ok(new { success = true, status = 200, message = $"Collabration with {email} is done" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("RemoveCollaborator/{NoteId}/{CollabId}")]
        public async Task<IActionResult> RemoveCollaborator(int NoteId, int CollabId)
        {
            try
            {
                var collab = _funDoNoteContext.Collaborators.FirstOrDefault(x => x.NoteId == NoteId);
                if (collab == null)
                {
                    return BadRequest(new { success = false, message = "note not found" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                bool isTrue = await _collaboratorBL.RemoveCollaborator(UserID, NoteId, CollabId);
                if (isTrue == true)
                {
                    return this.Ok(new { success = true, status = 200, message = $"Collaborated email {collab.collabEmail} Deleted successfully" });
                }
                return BadRequest(new { success = false, message = "Collabrator not found" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCollaboratorByUserId")]
        public async Task<IActionResult> GetCollaboratorByUserId()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                var info = await _collaboratorBL.GetCollaboratorsByUserId(UserID);
                return this.Ok(new { success = true, status = 200, AllCollab = info });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetCollaboratorByNoteId/{NoteId}")]
        public async Task<IActionResult> GetCollaboratorByNoteId(int NoteId)
        {
            try
            {
                var collab = _funDoNoteContext.Collaborators.FirstOrDefault(x => x.NoteId == NoteId);
                if (collab == null)
                {
                    return BadRequest(new { success = false, message = "no any collaborator for this note" });
                }
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = Int32.Parse(userid.Value);
                var info = await _collaboratorBL.GetCollaboratorsByNoteId(UserID, NoteId);
                return this.Ok(new { success = true, status = 200, AllCollab = info });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}