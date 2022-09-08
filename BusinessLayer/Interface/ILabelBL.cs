using CommonLayer.User;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        Task AddLabel(int UserId, int NoteId, string labelName);
        Task<Label> GetLabelsByNoteId(int UserId, int NoteId);
        Task<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId);
        List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId);
        Task UpdateLabel(int UserId, int NoteId, string newLabel);
        Task<bool> DeleteLabel(int UserId, int NoteId);
    }
}