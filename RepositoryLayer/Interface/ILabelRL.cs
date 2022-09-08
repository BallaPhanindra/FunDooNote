﻿using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Service.Entities;
namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public void AddLabel(int UserId, int NoteId, string labelName);
        Label GetLabelsByNoteId(int UserId, int NoteId);
        List<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId);
        List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId);
    }
}