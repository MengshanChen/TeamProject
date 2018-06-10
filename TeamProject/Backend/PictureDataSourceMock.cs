using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Backend
{
    /// <summary>
    /// Backend Mock DataSource for Picture, to manage them
    /// </summary>
    public class PictureDataSourceMock : IPictureInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile PictureDataSourceMock instance;
        private static object syncRoot = new Object();

        private PictureDataSourceMock() { }

        public static PictureDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new PictureDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// The Picture List
        /// </summary>
        private List<PictureModel> PictureList = new List<PictureModel>();

        /// <summary>
        /// Makes a new Picture
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Avatar Passed In</returns>
        public PictureModel Create(PictureModel data)
        {
            PictureList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public PictureModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = PictureList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public PictureModel Update(PictureModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = PictureList.Find(n => n.Id == data.Id);

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

            var myData = PictureList.Find(n => n.Id == Id);
            var myReturn = PictureList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Pictures</returns>
        public List<PictureModel> Index()
        {
            return PictureList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            PictureList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            Create(new PictureModel("default.png", "Default"));
            Create(new PictureModel("defaultboy.png", "Boy Deafult1"));
            Create(new PictureModel("defaultgirl.png", "Gril Default1"));
            Create(new PictureModel("defaultgirl1.png", "Girl Default2"));
            Create(new PictureModel("defaultboy1.png", "Boy Default2"));
            Create(new PictureModel("man03.jpg", "Joe", "1"));
            Create(new PictureModel("Olivia.jpg", "Olivia", "2"));
            Create(new PictureModel("girl1.jpg", "Erica", "3"));
            Create(new PictureModel("girl2.jpg", "Helena", "4"));
            Create(new PictureModel("boy1.jpg", "Daniel", "5"));
            Create(new PictureModel("boy2.jpg", "Victor", "6"));
            Create(new PictureModel("man04.jpg", "Kyle", "7"));
            Create(new PictureModel("girl3.jpg", "Raelee", "8"));
            Create(new PictureModel("girl4.jpg", "Cristina", "9"));
            Create(new PictureModel("man05.jpg", "Jack", "10"));
        }
    }
}