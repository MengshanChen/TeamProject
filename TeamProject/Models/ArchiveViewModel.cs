using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Models
{
    /// <summary>
    /// ViewModel for the Archive Model for the list of archived student data
    /// </summary>
    public class ArchiveViewModel
    {
        /// <summary>
        /// The List of archived students
        /// </summary>
        public List<ArchiveModel> ArciveList = new List<ArchiveModel>();

        /// <summary>
        /// Default constructor, needed becase of the constructor that takes a List of Archive Models
        /// </summary>
        public ArchiveViewModel() { }

        /// <summary>
        /// Take the data list passed in, and convert each to a new ArchiveModel item and add that to the ArchiveList
        /// </summary>
        /// <param name="dataList"></param>
        public ArchiveViewModel(List<ArchiveModel> dataList)
        {
            foreach (var item in dataList)
            {
                ArciveList.Add(item);
            }
        }
    }
}