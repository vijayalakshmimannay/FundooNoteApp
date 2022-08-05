using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity AddNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> GetNotes(long userId);
        public bool DeleteNotes(long userId, long noteId);
        public NotesEntity UpdateNote(NotesModel noteModel, long NoteId, long userId);
        public bool PinToDashboard(long NoteID, long userId);
        public bool Archive(long NoteID, long userId);
        public bool Trash(long NoteID, long userId);
        public NotesEntity Colour(long NoteID, string colour);
        public string AddImage(long NoteID, long userId, IFormFile image);


    }
}
