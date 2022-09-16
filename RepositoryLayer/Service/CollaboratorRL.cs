using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {
        FundooNoteContext _funDoNoteContext;
        private IConfiguration _config;
        public CollaboratorRL(FundooNoteContext funDoNoteContext, IConfiguration config)
        {
            _funDoNoteContext = funDoNoteContext;
            _config = config;
        }
        public async Task<Collaborator> AddCollaborator(int UserId, int NoteId, string email)
        {
            try
            {
                var user = _funDoNoteContext.users.Where(x => x.UserId == UserId).FirstOrDefault();
                var note = _funDoNoteContext.Note.FirstOrDefault(x => x.NoteId == NoteId);
                Collaborator collaborator = new Collaborator();
                if (note.IsTrash == true)
                {
                    return null;
                }
                collaborator.userId = UserId;
                collaborator.NoteId = NoteId;
                collaborator.collabEmail = email;
                await _funDoNoteContext.AddAsync(collaborator);
                _funDoNoteContext.SaveChanges();
                return collaborator;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> RemoveCollaborator(int UserId, int NoteId, int collabId)
        {
            try
            {
                var collab = _funDoNoteContext.Collaborators.Where(x => x.userId == UserId && x.NoteId == NoteId && x.collaboratorId == collabId).FirstOrDefault();
                if (collab == null)
                {
                    return false;
                }
                _funDoNoteContext.Collaborators.Remove(collab);
                await _funDoNoteContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorsByUserId(int UserId)
        {
            try
            {
                var user = await _funDoNoteContext.Collaborators.Where(x => x.userId == UserId).Include(x => x.user).Include(x => x.Note).ToListAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Collaborator>> GetCollaboratorsByNoteId(int UserId, int NoteId)
        {
            try
            {
                var collab = await _funDoNoteContext.Collaborators.Where(x => x.userId == UserId && x.NoteId == NoteId).Include(x => x.Note).Include(x => x.user).ToListAsync();
                return collab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}