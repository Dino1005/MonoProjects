using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mono.WebApi.Models
{
    public class AdvertisementView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }

        public AdvertisementView(Guid id, string title, DateTime uploadDate)
        {
            Id = id;
            Title = title;
            UploadDate = uploadDate;
        }
    }
}