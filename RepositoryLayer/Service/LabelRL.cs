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

namespace RepositoryLayer.Services
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
    }
}