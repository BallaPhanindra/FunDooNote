using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void UpdateNote(UpdateNoteModel updateNoteModel, int UserId, int NoteId)
        {
            try
                {
                    var note = _noteContext.Note.Where(x => x.NoteId == NoteId).FirstOrDefault();

                    note.Title = updateNoteModel.Title != "string" ? updateNoteModel.Title : note.Title;
                    note.Description = updateNoteModel.Description != "string" ? updateNoteModel.Description : note.Description;
                    note.Color = updateNoteModel.Color != "string" ? updateNoteModel.Color : note.Color;
                    note.IsPin = updateNoteModel.IsPin;
                    note.IsReminder = updateNoteModel.IsReminder;
                    note.IsArchieve = updateNoteModel.IsArchieve;
                    note.IsTrash = updateNoteModel.IsArchieve;
                    note.Reminder = updateNoteModel.Reminder;
                    note.ModifiedDate = DateTime.Now;
                    _noteContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
    }
}
