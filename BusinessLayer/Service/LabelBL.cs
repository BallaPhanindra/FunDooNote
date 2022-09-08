using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        ILabelRL _levelRL;
        public LabelBL(ILabelRL levelRL)
        {
            this._levelRL = levelRL;
        }

        public async Task AddLabel(int UserId, int NoteId, string labelName)
        {
            try
            {
                await this._levelRL.AddLabel(UserId, NoteId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Label> GetLabelsByNoteId(int UserId, int NoteId)
        {
            try
            {
                return await this._levelRL.GetLabelsByNoteId(UserId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<GetLabelModel> GetLabelByNoteIdwithJoin(int UserId, int NoteId)
        {
            try
            {
                return await this._levelRL.GetLabelByNoteIdwithJoin(UserId, NoteId);
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
                return this._levelRL.GetLabelByUserIdWithJoin(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateLabel(int UserId, int NoteId, string newLabel)
        {
            try
            {
                await this._levelRL.UpdateLabel(UserId, NoteId, newLabel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteLabel(int UserId, int NoteId)
        {
            try
            {
                return await this._levelRL.DeleteLabel(UserId, NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}