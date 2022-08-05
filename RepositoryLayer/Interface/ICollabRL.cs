using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity CreateCollab(long NoteID, string CollabEMail);
        public IEnumerable<CollabEntity> GetCollab(long userID);
    }
}
