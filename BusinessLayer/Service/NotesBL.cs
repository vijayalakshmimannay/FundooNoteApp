using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Service
{
     public class NotesBL : INotesBL
        {
            private readonly INotesRL iNotesRL;

            public NotesBL(INotesRL iNotesRL)
            {
                this.iNotesRL = iNotesRL;
            }

            public NotesEntity AddNotes(NotesModel notesModel, long userId)
            {
                try
                {
                    return iNotesRL.AddNotes(notesModel, userId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
       
    }

