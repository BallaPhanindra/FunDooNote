using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entities;

namespace BusinessLayer.Service
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL _collaboratorRL;
        public CollaboratorBL(ICollaboratorRL collaboratorRL)
        {
            _collaboratorRL = collaboratorRL;
        }
        public async Task<Collaborator> AddCollaborator(int UserId, int NoteId, string email)
        {
            try
            {
                return await _collaboratorRL.AddCollaborator(UserId, NoteId, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> RemoveCollaborator(int UserId, int NoteId, int CollabId)
        {
            try
            {
                return await _collaboratorRL.RemoveCollaborator(UserId, NoteId, CollabId);
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
                return await _collaboratorRL.GetCollaboratorsByUserId(UserId);
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
                return await _collaboratorRL.GetCollaboratorsByNoteId(UserId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}