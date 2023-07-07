using System;

namespace Projects.Model.Common
{
    public interface IAdvertisement
    {
        Guid Id { get; set; }
        string Title { get; set; }
        DateTime? UploadDate { get; set; }
        Guid? CategoryId { get; set; }
        Guid? PriorityId { get; set; }
        Guid? AccountId { get; set; }
    }
}
