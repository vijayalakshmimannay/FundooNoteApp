using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        
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

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        
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

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteModel">The note model.</param>
        /// <param name="NoteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        
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

        /// <summary>
        /// Pins to dashboard.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        
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

        /// <summary>
        /// Archives the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        
        public bool Archive(long NoteID, long userId)
        {
            try
            {
                return iNotesRL.Archive(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Trashes the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        
        public bool Trash(long NoteID, long userId)
        {
            try
            {
                return iNotesRL.Trash(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Colours the specified note identifier.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="colour">The colour.</param>
        /// <returns></returns>
        public NotesEntity Colour(long NoteID, string colour)
        {
            try
            {
                return iNotesRL.Colour(NoteID, colour);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public string AddImage(long NoteID, long userId, IFormFile image)
        {
            try
            {
               return iNotesRL.AddImage(NoteID, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
       
}

