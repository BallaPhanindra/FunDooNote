using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        readonly INoteRL _noteRL;
        public NoteBL(INoteRL noteRL)
        {
            _noteRL = noteRL;
        }
        public void AddNote(NoteModel noteModel, int UserId)
        {
            try
            {
                this._noteRL.AddNote(noteModel, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}