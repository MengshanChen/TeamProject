using System;
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
        /// The first name for the student, but does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Name", Description = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// The last name for the student, but does not need to be directly associated with the actual student name
        /// </summary>
        [Display(Name = "Last Name", Description = "Last Name")]
        [Required(ErrorMessage = "Name is required")]
        public string LastName { get; set; }

        /// <summary>
        /// The email of the student
        /// </summary>
        [Display(Name = "Email", Description = "Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /// <summary>
        /// The ID of the Picture the student is associated with, this will convert to a picture
        /// </summary>
        [Display(Name = "Picture", Description = "Picture")]
        [Required(ErrorMessage = "Picture is required")]
        public string PictureId { get; set; }

        /// <summary>
        /// DateTime of last transaction recorded, used for login and logout
        /// </summary>
        [Display(Name = "Date", Description = "Date and Time")]
        public DateTime LastDateTime { get; set; }

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
            Email = "N/A";
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
        /// Constructor for Student. Call this when recover student from archive
        /// </summary>
        /// <param name="data"> Archive data to use</param>
        public StudentModel(ArchiveModel data)
        {
            Id = data.Id;
            Name = data.FName;
            LastName = data.LName;
            PictureId = PictureBackend.Instance.GetFirstPictureId();
        }

        /// <summary>
        /// Constructor for Student.  Call this when making a new student
        /// </summary>
        /// <param name="name">The Name to call the student</param>
        /// <param name="avatarId">The picture to use, if not specified, will call the backend to get an ID</param>
        public StudentModel(string name, string lastname, string email, string pictureId)
        {
            Initialize();
            Email = email;
            Name = name;
            LastName = lastname;

            // If no picture ID is sent in, then go and get the first picture ID from the backend data as the default to use.
            if (string.IsNullOrEmpty(pictureId))
            {
                pictureId = PictureBackend.Instance.GetFirstPictureId();
            }
            PictureId = pictureId;
        }

        /// <summary>
        /// Convert a Student Display View Model, to a Student Model, used for when passed data from Views that use the full Student Display View Model.
        /// </summary>
        /// <param name="data">The student data to pull</param>
        public StudentModel(StudentDisplayViewModel data)
        {
            Id = data.Id;
            Name = data.Name;
            LastName = data.LastName;
            Email = data.Email;
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
            LastName = data.LastName;
            Email = data.Email;
            PictureId = data.PictureId;
            Status = data.Status;

            return true;
        }
    }

    /// <summary>
    /// For the Index View, add the Picture URI to the Student, so it shows the student with the picture
    /// </summary>
    public class StudentDisplayViewModel : StudentModel
    {
        /// <summary>
        /// The path to the local image for the picture
        /// </summary>
        [Display(Name = "Picture Image", Description = "Picture to Show")]
        public string PictureUri { get; set; }

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
            LastName = data.LastName;
            Email = data.Email;
            PictureId = data.PictureId;
            Status = data.Status;

            var myPicture = PictureBackend.Instance.Read(PictureId);
            if (myPicture == null)
            {
                // Nothing to convert
                return;
            }
            PictureUri = myPicture.Uri;
        }
    }
}