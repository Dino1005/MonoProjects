using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projects.WebApi.Models
{
    public class AdvertisementView
    {
        public string Title { get; set; }
        public DateTime? UploadDate { get; set; }

        public AdvertisementView(string title, DateTime? uploadDate)
        {
            Title = title;
            UploadDate = uploadDate;
        }
    }
}