namespace RestaurantMenu
{
    public class MenuMaster<T>
    {
        private ICollection<T> items;
        private int pageSize;
        public MenuMaster(ICollection<T> items, int pageSize)
        {
            if (items is null || !items.Any())
                throw new ArgumentException("Меню не может быть пустым");

            if (pageSize <= 0)
                throw new ArgumentException("Размер страницы должнен быть числом положительным");

            this.items = items;
            this.pageSize = pageSize;
        }

        public int GetItemsCount() => 
            items.Count;

        public int GetPagesCount() =>
            (int)Math.Ceiling(GetItemsCount() / (double)pageSize);

        public int GetPageItemsCount(int pageNumber)
        {
            ValidatePageNumber(pageNumber);
            return pageNumber < GetPagesCount() ? pageSize 
                : GetItemsCount() - pageSize * (pageNumber - 1);
        }

        public IEnumerable<T> GetPageItems(int pageNumber)
        {
            ValidatePageNumber(pageNumber);
            return items.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<T> GetFirstItemsOfPages() =>
            items.Where((x,i) => i % pageSize == 0);

        private void ValidatePageNumber(int pageNumber)
        {
            if (pageNumber > GetPagesCount() || pageNumber <= 0)
                throw new ArgumentException($"Значение страницы должно быть в диапазане [{1}; {GetPagesCount()}]");
        }
            
    }
}
