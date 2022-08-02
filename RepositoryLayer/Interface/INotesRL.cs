﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> GetNotes(long userId);
        public bool DeleteNotes(long userId, long noteId);
        public NotesEntity UpdateNote(NotesModel noteModel, long NoteId, long userId);
        public bool PinToDashboard(long NoteID, long userId);
    }
}