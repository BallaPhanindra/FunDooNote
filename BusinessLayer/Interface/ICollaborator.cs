using BusinessLayer.Service;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        Task<Collaborator> AddCollaborator(int UserId, int NoteId, string email);
        Task<bool> RemoveCollaborator(int UserId, int NoteId, int CollabId);
        Task<List<Collaborator>> GetCollaboratorsByUserId(int UserId);
        Task<List<Collaborator>> GetCollaboratorsByNoteId(int UserId, int NoteId);
    }
}