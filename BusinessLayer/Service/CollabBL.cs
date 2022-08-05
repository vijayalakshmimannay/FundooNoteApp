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
        public CollabEntity GetCollab(long userID)
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
