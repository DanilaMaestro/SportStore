using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStore.WebUI.Models
{
    public class PagingInfo
    {
        public string CurrentCategory { get; set; }
        public int CurrentPage { get; set; }

        public int ItemsCount { get; set; }
        public int ItemPerPage { get; set; }

        public int PageCount { get { return (int)Math.Ceiling((decimal)ItemsCount / ItemPerPage); } }
    }
}