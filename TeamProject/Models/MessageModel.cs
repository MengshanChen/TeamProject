using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeamProject.Models
{
    public class MessageModel
    {
        /// <summary>
        /// The ID for the Message, this is the key, and a required field
        /// </summary>
        [Key]
        [Display(Name = "Id", Description = "Message Id")]
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; }

        /// <summary>
        /// Message from visitor
        /// </summary>
        [Display(Name = "Message", Description = "Message from visitor")]
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }


        /// <summary>
        /// The default for the new archive student info
        /// </summary>
        public void Initialize()
        {
            Id = Guid.NewGuid().ToString();
        }


        public MessageModel()
        {
            Initialize();
            Message = "Leave Message Here";
        }

        public MessageModel(string message)
        {
            Initialize();
            Message = message;
        }

        public MessageModel(MessageModel data)
        {
            Id = data.Id;
            Message = data.Message;
        }

        /// <summary>
        /// Update Message with the values passed in.
        /// </summary>
        /// <param name="data">The values to update</param>
        /// <returns>False if null, else true</returns>
        public bool Update(MessageModel data)
        {
            if (data == null)
            {
                return false;
            }
            Message = data.Message;
            return true;
        }
    }
}