﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    { 
    private readonly FundooContext fundooContext;
    private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The fundoo context.</param>
        /// <param name="configuration">The configuration.</param>
        public NotesRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
      
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
                NotesEntity notesEntity = new NotesEntity();
                var result = fundooContext.UserEntities.Where(e => e.UserId == userId);
                if (result != null)
                {
                    notesEntity.UserId = userId;
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Reminder = notesModel.Reminder;
                    notesEntity.Colour = notesModel.Colour;
                    notesEntity.Image = notesModel.Image;
                    notesEntity.Archive = notesModel.Archive;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Edited = notesModel.Edited;
                    
                    fundooContext.NotesTable.Add(notesEntity);
                    fundooContext.SaveChanges();
                    return notesEntity;
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
        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<NotesEntity> GetNotes(long userId)
        {
            try
            {
                var result = this.fundooContext.NotesTable.Where(e => e.UserId == userId);
                return result;
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

                var result = fundooContext.NotesTable.Where(e => e.UserId == userId && e.NoteID == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    this.fundooContext.SaveChanges();
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
                var result = fundooContext.NotesTable.Where(note => note.UserId == userId && note.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = noteModel.Title;
                    result.Description = noteModel.Description;
                    result.Reminder = noteModel.Reminder;
                    result.Edited = DateTime.Now;
                    result.Colour = noteModel.Colour;
                    result.Image = noteModel.Image;

                    this.fundooContext.SaveChanges();
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
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
                var findNotes = fundooContext.NotesTable.First(e => e.NoteID == NoteID);
                if (findNotes != null)
{
                    findNotes.Colour = colour;
                    fundooContext.SaveChanges();
                    return findNotes;
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

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="NoteID">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public string AddImage(long NoteID, long userId, IFormFile image)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(this.configuration["CloudinaryAccount:CloudName"], this.configuration["CloudinaryAccount:APIKey"], this.configuration["CloudinaryAccount:APISecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image Upload Successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
    
    

    
}

