using FluentAssertions;
using NUnit.Framework;
using RestaurantMenu;

namespace MenuMasterTest
{
    internal abstract class BaseMenuMasterTest<T>
    {
        private MenuMaster<T> menuMaster;

        #region валидация в конструкторе
        [TestCase(0)] 
        [TestCase(-1)]
        public void MenuMaster_Constructor_PageSizeLessOne_ExpectError(int pageSize)
        {
            Action act = () => new MenuMaster<T>(new T[5], pageSize);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Размер страницы должнен быть числом положительным");
        }

        [Test]
        public void MenuMaster_Constructor_EmptyItems_ExpectError()
        {
            Action act = () => new MenuMaster<T>(new T[0], 3);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Меню не может быть пустым");
        }

        [Test]
        public void MenuMaster_Constructor_NullItems_ExpectError()
        {
            Action act = () => new MenuMaster<T>(null, 3);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Меню не может быть пустым");
        }

        [TestCase(5,2)]
        [TestCase(10, 5)]
        [TestCase(1, 50000)]
        public void MenuMaster_Constructor_PageSizeGreaterZero(int itemsCount, int pageSize)
        {
            Action act = () => new MenuMaster<T>(new T[itemsCount], pageSize);
            act.Should().NotThrow();
        }
        #endregion

        #region GetItemsCount
        [TestCase(10, 5)]
        [TestCase(10, 10)]
        [TestCase(1, 10)]
        public void MenuMaster_GetItemsCountTest(int itemsCount, int pageSize)
        {
            menuMaster = new MenuMaster<T>(new T[itemsCount], pageSize);

            menuMaster.GetItemsCount().Should().Be(itemsCount);
        }
        #endregion

        #region GetPagesCount
        [TestCase(10, 5, 2)]
        [TestCase(10, 3, 4)]
        [TestCase(1, 3, 1)]
        public void MenuMaster_GetPagesCountTest(int itemsCount, int pageSize, int pageCount)
        {
            menuMaster = new MenuMaster<T>(new T[itemsCount], pageSize);

            menuMaster.GetPagesCount().Should().Be(pageCount);
        }
        #endregion

        #region GetPageItemsCount
        [TestCase(10, 3, 1, 3)]
        [TestCase(10, 3, 4, 1)]
        public void MenuMaster_GetPageItemsCountTest(int itemsCount, int pageSize, int pageNumber, int pageItemsCount)
        {
            menuMaster = new MenuMaster<T>(new T[itemsCount], pageSize);

            menuMaster.GetPageItemsCount(pageNumber).Should().Be(pageItemsCount);
        }

        [TestCase(10, 3, 0)]
        [TestCase(10, 3, 5)]
        [TestCase(10, 5, 3)]
        [TestCase(1, 10, 2)]
        public void MenuMaster_GetPageItemsCount_PageNumberLessOneOrGreaterPageCount_ExpectError(
            int itemsCount, int pageSize, int pageNumber)
        {
            menuMaster = new MenuMaster<T>(new T[itemsCount], pageSize);

            Action act = () => menuMaster.GetPageItemsCount(pageNumber);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Значение страницы должно быть в диапазане [{1}; {menuMaster.GetPagesCount()}]");
        }
        #endregion

        #region GetPageItems
        [TestCase(10, 3, 1, 0, 1, 2)]
        [TestCase(10, 3, 4, 9)]
        public void MenuMaster_GetPageItemsTest(int itemsCount, int pageSize, int pageNumber, params int[] indexes)
        {
            var items = new T[itemsCount];

            menuMaster = new MenuMaster<T>(items, pageSize);

            menuMaster.GetPageItems(pageNumber).Should()
                .Equal(items.Where((x,i) => indexes.Contains(i)));
        }

        [TestCase(4, 3, 0)]
        [TestCase(4, 3, 3)]
        public void MenuMaster_GetPageItems_PageNumberLessOneOrGreaterPageCount_ExpectError(int itemsCount, int pageSize, int pageNumber)
        {
            menuMaster = new MenuMaster<T>(new T[itemsCount], pageSize);

            Action act = () => menuMaster.GetPageItems(pageNumber);

            act.Should().Throw<ArgumentException>()
                .WithMessage($"Значение страницы должно быть в диапазане [{1}; {menuMaster.GetPagesCount()}]");
        }
        #endregion

        #region GetFirstItemsOfPages
        [TestCase(10, 3, 0, 3, 6, 9)]
        [TestCase(4, 2, 0, 2)]
        [TestCase(1, 2, 0)]
        public void MenuMaster_GetFirstItemsOfPages(int itemsCount, int pageSize, params int[] indexes)
        {
            var items = new T[itemsCount];

            menuMaster = new MenuMaster<T>(items, pageSize);

            menuMaster.GetFirstItemsOfPages().Should().Equal(items.Where((x,i)=>indexes.Contains(i)));
        }
        #endregion
    }
}
