using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMenu
{
    internal class MenuMaster<T>
    {
        private ICollection<T> items;
        private int pageSize;
        public MenuMaster(ICollection<T> items, int pageSize)
        {
            this.items = items;
            this.pageSize = pageSize;
        }

        public int GetItemsCount() => 
            items.Count;

        public int GetPageCount() =>
            (int)Math.Ceiling(GetItemsCount()/(double)pageSize);

        public int GetPageItemsCount(int pageNumber) => 
            pageNumber < GetPageCount() ? pageSize : GetItemsCount() - pageSize * (pageNumber - 1);

        public IEnumerable<T> GetPageItems(int pageNumber) =>
            items.Skip((pageNumber-1)*pageSize).Take(pageSize);

        public IEnumerable<T> GetFirstItemsAllPages() =>
            items.Where((x,i)=>i%pageSize==0);
    }
}
