using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Backend
{
        /// <summary>
        /// Datasource Interface for Archive
        /// </summary>
        public interface IArchiveInterface
        {
            ArchiveModel Create(ArchiveModel data);
            ArchiveModel Read(string id);
            ArchiveModel Update(ArchiveModel data);
            bool Delete(string id);
            List<ArchiveModel> Index();
            void Reset();
        }
    }