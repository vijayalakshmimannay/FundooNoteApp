﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public bool CreateLabel(string name, long noteID, long userID);
        public IEnumerable<LabelEntity> GetLabel(long labelID);

       
        public bool UpdateLabel(string name, long labelID);
        public bool RemoveLabel(long LabelID);
      

    }
}
