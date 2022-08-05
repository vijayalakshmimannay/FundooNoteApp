using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;

        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity CreateCollab(long NoteID, string CollabEMail)
        {
            try
            {
                var noteResult = fundooContext.NotesTable.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var emailResult = fundooContext.UserEntities.Where(x => x.Email == CollabEMail).FirstOrDefault();
                if (noteResult != null && emailResult != null)
                {
                    CollabEntity collabEntity = new CollabEntity();
                    collabEntity.NoteID = noteResult.NoteID;
                    collabEntity.CollabEMail = emailResult.Email;
                    collabEntity.UserId = emailResult.UserId;
                    fundooContext.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
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

        public CollabEntity GetCollab(long userID)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(x => x.UserId == userID).FirstOrDefault();
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
        public bool RemoveCollab(long CollabID, long userID)
        {
            try
            {
                var result = fundooContext.CollabTable.Where(x => x.CollabID == CollabID && x.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.CollabTable.Remove(result);
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
    }
}
