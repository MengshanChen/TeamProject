using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Models
{
    public class MessageViewModel
    {
        /// <summary>
        /// The message List to return to the View
        /// </summary>
        public List<MessageModel> MessageList = new List<MessageModel>();

        /// <summary>
        /// Default constructor, needed becase of the constructor that takes a List of message Models
        /// </summary>
        public MessageViewModel() { }

        /// <summary>
        /// Take the data list passed in, and convert each to a new MessageModel item and add that to the MessageList
        /// </summary>
        /// <param name="dataList"></param>
        public MessageViewModel(List<MessageModel> dataList)
        {
            foreach (var item in dataList)
            {
                MessageList.Add(new MessageModel(item));
            }
        }
    }
}