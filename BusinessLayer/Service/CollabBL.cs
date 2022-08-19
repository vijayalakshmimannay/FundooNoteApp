using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL iCollabRL;
        public CollabBL(ICollabRL iCollabRL)
        {
            this.iCollabRL = iCollabRL;
        }

        /// <summary>
        /// Creates the collab.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="CollabEMail">The collab e mail.</param>
        /// <returns></returns>
        
        public CollabEntity CreateCollab(long NoteID, string CollabEMail)
        {
            try
            {
                return iCollabRL.CreateCollab(NoteID, CollabEMail);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the collab.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        
/        public CollabEntity GetCollab(long userID)
        {
            try
            {
                return iCollabRL.GetCollab(userID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Removes the collab.
        /// </summary>
        /// <param name="CollabID">The collab identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        
        public bool RemoveCollab(long CollabID, long userID)
        {
            try
            {
                return iCollabRL.RemoveCollab(CollabID, userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
