using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
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
                return iLabelRL.CreateLabel(name, noteID, userID);
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
                return iLabelRL.GetLabel(labelID);
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
                return iLabelRL.UpdateLabel(name, labelID);
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
                return iLabelRL.RemoveLabel(LabelID);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
    }
}

