using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public bool CreateLabel(string name, long noteID, long userID);
        public IEnumerable<LabelEntity> GetLabel(long labelID);

      
        public bool RemoveLabel(long LabelID);
        public bool UpdateLabel(string name, long labelID);


    }
}
