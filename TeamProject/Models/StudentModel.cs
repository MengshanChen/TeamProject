﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TeamProject.Backend;

namespace TeamProject.Models
{
    /// <summary>
    /// The Student, this holds the student id, name, tokens.  Other things about the student such as their attendance is pulled from the attendance data, or the owned items, from the inventory data
    /// </summary>
    public class StudentModel
    {
        /// <summary>
        /// The ID for the Student, this is the key, and a required field
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Student Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        /// <summary>
        /// The name for the student, but does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// The ID of the Avatar the student is associated with, this will convert to an avatar picture
        /// </summary>
        [Display(Name = "PictureId", Description = "Picture")]
        [Required(ErrorMessage = "Picture is required")]
        public string AvatarId { get; set; }

        /// <summary>
        /// The status of the student, for example currently logged in, out
        /// </summary>
        [Display(Name = "Current Status", Description = "Status of the Student")]
        [Required(ErrorMessage = "Status is required")]
        public StudentStatusEnum Status { get; set; }

        /// <summary>
        /// The defaults for a new student
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
            Status = StudentStatusEnum.Out;
        }

        /// <summary>
        /// Constructor for a student
        /// </summary>
        public StudentModel()
        {
            Initialize();
        }

        /// <summary>
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        /// <param name="avatarId">The avatar to use, if not specified, will call the backend to get an ID</param>
        public StudentModel(string name, string avatarId)
        {
            Initialize();

            Name = name;

            // If no avatar ID is sent in, then go and get the first avatar ID from the backend data as the default to use.
            if (string.IsNullOrEmpty(avatarId))
            {
                avatarId = PictureBackend.Instance.GetFirstPictureId();
            }
            AvatarId = avatarId;
        }

        /// <summary>
        /// Convert a Student Display View Model, to a Student Model, used for when passed data from Views that use the full Student Display View Model.
        /// </summary>
        /// <param name="data">The student data to pull</param>
        public StudentModel(StudentDisplayViewModel data)
        {
            Id = data.Id;
            Name = data.Name;
            PictureId = data.PictureId;
            Status = data.Status;
        }

        /// <summary>
        /// Update the Data Fields with the values passed in, do not update the ID.
        /// </summary>
        /// <param name="data">The values to update</param>
        /// <returns>False if null, else true</returns>
        public bool Update(StudentModel data)
        {
            if (data == null)
            {
                return false;
            }

            Name = data.Name;
            AvatarId = data.AvatarId;
            AvatarLevel = data.AvatarLevel;
            Tokens = data.Tokens;
            Status = data.Status;

            return true;
        }
    }

    /// <summary>
    /// For the Index View, add the Avatar URI to the Student, so it shows the student with the picture
    /// </summary>
    public class StudentDisplayViewModel : StudentModel
    {
        /// <summary>
        /// The path to the local image for the avatar
        /// </summary>
        [Display(Name = "Avatar Picture", Description = "Avatar Picture to Show")]
        public string AvatarUri { get; set; }

        /// <summary>
        /// Display name for the Avatar on the student information (Friendly Police etc.)
        /// </summary>
        [Display(Name = "Avatar Name", Description = "Avatar Name")]
        public string AvatarName { get; set; }

        /// <summary>
        /// Description of the Avatar to show on the student information
        /// </summary>
        [Display(Name = "Avatar Description", Description = "Avatar Description")]
        public string AvatarDescription { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public StudentDisplayViewModel() { }

        /// <summary>
        /// Creates a Student Display View Model from a Student Model
        /// </summary>
        /// <param name="data">The Student Model to create</param>
        public StudentDisplayViewModel(StudentModel data)
        {
            Id = data.Id;
            Name = data.Name;
            PictureId = data.PictureId;
            Status = data.Status;

            var myPicture = PictureBackend.Instance.Read(PictureId);
            if (myPicture == null)
            {
                // Nothing to convert
                return;
            }

            AvatarName = myDataAvatar.Name;
            AvatarDescription = myDataAvatar.Description;
            AvatarUri = myDataAvatar.Uri;
        }
    }
}