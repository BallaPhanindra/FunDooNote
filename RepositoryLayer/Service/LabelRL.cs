using RepositoryLayer.Interface;
using System;
using RepositoryLayer.Service.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Service;
using CommonLayer.User;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        FundooNoteContext _funDoNoteContext;
        private IConfiguration _config;
        public LabelRL(FundooNoteContext funDoNoteContext, IConfiguration config)
        {
            _funDoNoteContext = funDoNoteContext;
            this._config = config;
        }

        public void AddLabel(int UserId, int NoteId, string labelName)
        {
            try
            {
                Label _label = new Label();

                _label.UserId = UserId;
                _label.NoteId = NoteId;
                _label.LabelName = labelName;
                _funDoNoteContext.Labels.Add(_label);
                _funDoNoteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Label GetLabelsByNoteId(int UserId, int NoteId)
        {
            try
            {
                var user = _funDoNoteContext.users.Where(x => x.UserId == UserId).FirstOrDefault();
                var note = _funDoNoteContext.Note.Where(x => x.NoteId == NoteId && x.UserId == UserId).FirstOrDefault();
                var label = _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefault();

                if (label == null)
                {
                    return null;
                }

                return _funDoNoteContext.Labels.Where(x => x.NoteId == NoteId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId)
        {
            try
            {
                var label = this._funDoNoteContext.Labels.FirstOrDefault(x => x.UserId == UserId);
                var result = (from user in _funDoNoteContext.users
                              join notes in _funDoNoteContext.Note on user.UserId equals UserId //where notes.NoteId == NoteId
                              join labels in _funDoNoteContext.Labels on notes.NoteId equals labels.NoteId
                              where labels.NoteId == NoteId && labels.UserId == UserId
                              select new GetLabelModel
                              {

                                  UserId = UserId,
                                  NoteId = notes.NoteId,
                                  Title = notes.Title,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Email = user.Email,
                                  Description = notes.Description,
                                  Color = notes.Color,
                                  LabelName = labels.LabelName,
                                  CreatedDate = labels.user.CreatedDate
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetLabelModel> GetLabelByUserIdWithJoin(int UserId)
        {
            try
            {
                var label = this._funDoNoteContext.Labels.FirstOrDefault(x => x.UserId == UserId);
                var result = (from user in _funDoNoteContext.users
                              join notes in _funDoNoteContext.Note on user.UserId equals UserId //where notes.NoteId == NoteId
                              join labels in _funDoNoteContext.Labels on notes.NoteId equals labels.NoteId
                              where labels.UserId == UserId
                              select new GetLabelModel
                              {

                                  UserId = UserId,
                                  NoteId = notes.NoteId,
                                  Title = notes.Title,
                                  FirstName = user.FirstName,
                                  LastName = user.LastName,
                                  Email = user.Email,
                                  Description = notes.Description,
                                  Color = notes.Color,
                                  LabelName = labels.LabelName,
                                  CreatedDate = labels.user.CreatedDate
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}