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
        public interface IQAInterface
        {
            MessageModel Create(MessageModel data);
            MessageModel Read(string id);
            MessageModel Update(MessageModel data);
            bool Delete(string id);
            List<MessageModel> Index();
            void Reset();
        }
    }