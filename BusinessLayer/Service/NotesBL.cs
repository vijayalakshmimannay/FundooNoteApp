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
             public IEnumerable<NotesEntity> GetNotes(long userId)
             {
                 try
                 {
                    return iNotesRL.GetNotes(userId);
                 }
                 catch (Exception)
                 {
                  throw;
                 }
             }
             public bool DeleteNotes(long userId, long noteId)
             {
                 try
                 {
                     return iNotesRL.DeleteNotes(userId, noteId);
                 }
                  catch (Exception)
                 {
                   throw;
                 }
             }

              public NotesEntity UpdateNote(NotesModel noteModel, long NoteId, long userId)
              {
                   try
                   {
                      return iNotesRL.UpdateNote(noteModel, NoteId, userId);
                   }
                   catch (Exception)
                   {
                      throw;
                   }
              }
              public bool PinToDashboard(long NoteID, long userId)
              {
                   try
                   { 
                       return iNotesRL.PinToDashboard(NoteID, userId);
                   }
                   catch (Exception)
                   {
                       throw;
                   }
              }

     }
       
}

