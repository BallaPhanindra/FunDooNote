using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.User;
namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteId);
        public bool DeleteNote(int UserId, int NoteId);
    }
}