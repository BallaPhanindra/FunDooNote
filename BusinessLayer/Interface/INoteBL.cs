using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.User;
using RepositoryLayer.Service.Entities;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteId);
        public bool DeleteNote(int UserId, int NoteId);
        public Note GetNote(int UserId, int NoteId);
        public List<Note> GetAllNotes(int UserId);
        public List<NoteResponseModel> GetAllNotesUsingJoin(int UserId);
    }
}