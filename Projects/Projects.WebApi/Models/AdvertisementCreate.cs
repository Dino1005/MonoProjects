using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mono.WebApi.Models
{
    public class AdvertisementCreate
    {
        public Guid Id { get; set; }
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
            Id = Guid.NewGuid();
            Title = title;
            UploadDate = uploadDate;
            CategoryId = categoryId;
            PriorityId = priorityId;
            AccountId = accountId;
        }
    }
}