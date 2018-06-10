using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Backend
{
    public class QADataSourceMock : IQAInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile QADataSourceMock instance;
        private static object syncRoot = new Object();

        private QADataSourceMock() { }

        public static QADataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new QADataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }


        /// <summary>
        /// The Message list
        /// </summary>
        private List<MessageModel> MessageList = new List<MessageModel>();

        /// <summary>
        /// Create new Message with exisitng MessageModel data
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Avatar Passed In</returns>
        public MessageModel Create(MessageModel data)
        {
            MessageList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public MessageModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = MessageList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public MessageModel Update(MessageModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = MessageList.Find(n => n.Id == data.Id);

            myReturn.Update(data);

            return myReturn;
        }

        /// <summary>
        /// Remove the Data item if it is in the list
        /// </summary>
        /// <param name="data"></param>
        /// <returns>True for success, else false</returns>
        public bool Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return false;
            }

            var myData = MessageList.Find(n => n.Id == Id);
            var myReturn = MessageList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Message</returns>
        public List<MessageModel> Index()
        {
            return MessageList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            MessageList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            Create(new MessageModel("How to update student photo?"));
            Create(new MessageModel("How to play the WorldTour Game?"));
            Create(new MessageModel("How to reset my password?"));
            Create(new MessageModel("How many hours required to graduate?"));
            Create(new MessageModel("How to generate my attendance report?"));
            Create(new MessageModel("How to delete my account?"));
        }
    }
}