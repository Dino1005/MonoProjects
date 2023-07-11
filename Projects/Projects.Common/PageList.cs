using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Common
{
    public class PageList<T> where T : class
    {
        public List<T> Items { get;}
        public int TotalCount { get;}

        public PageList(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
