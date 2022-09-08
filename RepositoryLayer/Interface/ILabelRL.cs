using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Service.Entities;
namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task AddLabel(int UserId, int NoteId, string labelName);
        Task<Label> GetLabelsByNoteId(int UserId, int NoteId);
        Task<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId);
        List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId);
        Task UpdateLabel(int UserId, int NoteId, string newLabel);
    }
}