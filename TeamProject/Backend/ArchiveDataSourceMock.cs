using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Backend
{
    public class ArchiveDataSourceMock : IArchiveInterface
    {
        /// <summary>
        /// Make into a Singleton
        /// </summary>
        private static volatile ArchiveDataSourceMock instance;
        private static object syncRoot = new Object();

        private ArchiveDataSourceMock() { }

        public static ArchiveDataSourceMock Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ArchiveDataSourceMock();
                            instance.Initialize();
                        }
                    }
                }

                return instance;
            }
        }


        /// <summary>
        /// The Archived Students List
        /// </summary>
        private List<ArchiveModel> ArchiveList = new List<ArchiveModel>();

        /// <summary>
        /// Create new Archive studnet
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Avatar Passed In</returns>
        public ArchiveModel Create(ArchiveModel data)
        {
            ArchiveList.Add(data);
            return data;
        }

        /// <summary>
        /// Return the data for the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null or valid data</returns>
        public ArchiveModel Read(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var myReturn = ArchiveList.Find(n => n.Id == id);
            return myReturn;
        }

        /// <summary>
        /// Update all attributes to be what is passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Null or updated data</returns>
        public ArchiveModel Update(ArchiveModel data)
        {
            if (data == null)
            {
                return null;
            }
            var myReturn = ArchiveList.Find(n => n.Id == data.Id);

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

            var myData = ArchiveList.Find(n => n.Id == Id);
            var myReturn = ArchiveList.Remove(myData);
            return myReturn;
        }

        /// <summary>
        /// Return the full dataset
        /// </summary>
        /// <returns>List of Avatars</returns>
        public List<ArchiveModel> Index()
        {
            return ArchiveList;
        }

        /// <summary>
        /// Reset the Data, and reload it
        /// </summary>
        public void Reset()
        {
            ArchiveList.Clear();
            Initialize();
        }

        /// <summary>
        /// Create Placeholder Initial Data
        /// </summary>
        public void Initialize()
        {
            Create(new ArchiveModel("Xavier", "Romero", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("John", "Jones", "2017", GraduateStatusEnum.Drop));
            Create(new ArchiveModel("Tracey", "Smith", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Anne", "McNeil", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Gillian", "Carpenter", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Karen", "Rogers", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Amy", "Sanders", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Kevin", "White", "2017", GraduateStatusEnum.Drop));
            Create(new ArchiveModel("Mary", "Brown", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Andrew", "Smith", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("James", "Francis", "2017", GraduateStatusEnum.Drop));
            Create(new ArchiveModel("Karen", "Joens", "2017", GraduateStatusEnum.Graduate));
            Create(new ArchiveModel("Jenny", "Smith", "2017", GraduateStatusEnum.Graduate));
        }
    }
}