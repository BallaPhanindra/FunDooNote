using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        readonly FundooNoteContext _noteContext;

        public NoteRL(FundooNoteContext noteContext)
        {
            this._noteContext = noteContext;
        }

        public void AddNote(NoteModel noteModel, int UserId)
        {
            try
            {
                Note note = new Note();
                note.Title = noteModel.Title;
                note.Description = noteModel.Description;
                note.Color = noteModel.Color;
                note.UserId = UserId;
                note.Reminder = DateTime.Now;
                note.CreatedDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                _noteContext.Add(note);
                _noteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}