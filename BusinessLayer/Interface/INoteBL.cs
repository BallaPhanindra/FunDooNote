using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        Task<bool> ArchieveNote(int UserId, int NoteId);
        Task<bool> PinNote(int UserId, int NoteId);
        Task<bool> TrashNote(int UserId, int NoteId);
        Task<bool> ReminderNote(int UserId, int NoteId, DateTime reminder);
        Task<bool> DeleteReminderNote(int UserId, int NoteId);
        Task UpdateColor(int UserId, int NoteId, string Color);
        public List<GetColorModel> GetAllColour(int UserId);
    }
}