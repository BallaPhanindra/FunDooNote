using CommonLayer.User;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        void AddLabel(int UserId, int NoteId, string labelName);
        Label GetLabelsByNoteId(int UserId, int NoteId);
        List<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId);
        List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId);
    }
}