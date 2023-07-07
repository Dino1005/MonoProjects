using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projects.WebApi.Models
{
    public class AdvertisementCreate
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid PriorityId { get; set; }
        [Required]
        public Guid AccountId { get; set; }
        

        public AdvertisementCreate(string title, DateTime uploadDate, Guid categoryId, Guid priorityId, Guid accountId)
        {
            Title = title;
            UploadDate = uploadDate;
            CategoryId = categoryId;
            PriorityId = priorityId;
            AccountId = accountId;
        }
    }
}