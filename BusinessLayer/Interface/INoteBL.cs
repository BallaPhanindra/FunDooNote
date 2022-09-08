using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.User;
namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        void AddNote(NoteModel noteModel, int UserId);
    }
}