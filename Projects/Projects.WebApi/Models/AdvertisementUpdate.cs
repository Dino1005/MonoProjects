using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projects.WebApi.Models
{
    public class AdvertisementUpdate
    {
        public string Title { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? PriorityId { get; set; }

        public AdvertisementUpdate(string title, Guid? categoryId, Guid? priorityId)
        {
            Title = title;
            CategoryId = categoryId;
            PriorityId = priorityId;
        }
    }
}