using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        public bool CreateLabel(string name, long noteID, long userID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = name;
                    labelEntity.NoteID = result.NoteID;
                    labelEntity.UserId = result.UserId;
                    fundooContext.LabelTable.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns></returns>
        public IEnumerable<LabelEntity> GetLabel(long labelID)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelID == labelID);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <returns></returns>
        public bool UpdateLabel(string name, long labelID)
        {
            try
            {
                var result = fundooContext.LabelTable.First(x => x.LabelID == labelID);
                if (result != null)
                {
                    result.LabelName = name;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Removes the label.
        /// </summary>
        /// <param name="LabelID">The label identifier.</param>
        /// <returns></returns>
        public bool RemoveLabel(long LabelID)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(x => x.LabelID == LabelID).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.LabelTable.Remove(result);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

