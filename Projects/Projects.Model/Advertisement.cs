using System;
using Projects.Model.Common;

namespace Projects.Model
{
    public class Advertisement : IAdvertisement
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime? UploadDate { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? PriorityId { get; set; }
        public Guid? AccountId { get; set; }

        public Advertisement(Guid id, string title, DateTime? uploadDate, Guid? categoryId, Guid? priorityId, Guid? accountId)
        {
            Id = id;
            Title = title;
            UploadDate = uploadDate;
            CategoryId = categoryId;
            PriorityId = priorityId;
            AccountId = accountId;
        }
    }
}