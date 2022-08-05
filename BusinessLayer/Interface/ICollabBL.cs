using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(long NoteID, string CollabEMail);
        public IEnumerable<CollabEntity> GetCollab(long userID);
    }
}
