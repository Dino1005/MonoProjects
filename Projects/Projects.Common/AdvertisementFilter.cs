using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Common
{
    public class AdvertisementFilter
    {
        public string TitleQuery { get; set; }
        public DateTime? DateQuery { get; set; }
        public List<Guid> PriorityQuery { get; set; }
        public List<Guid> CategoryQuery { get; set; }
        public List<Guid> AccountQuery { get; set; }

        public AdvertisementFilter(string titleQuery, DateTime? dateQuery, List<Guid> priorityQuery, List<Guid> categoryQuery, List<Guid> accountQuery)
        {
            TitleQuery = titleQuery;
            DateQuery = dateQuery;
            PriorityQuery = priorityQuery;
            CategoryQuery = categoryQuery;
            AccountQuery = accountQuery;
        }
    }
}
