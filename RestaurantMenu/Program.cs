using System.Linq;
using RestaurantMenu;

var a = new string[5] {"a", "b", "c", "d", "e" };
var menu = new MenuMaster<string>(a, 3);

menu.GetFirstItemsAllPages().ToList().ForEach(Console.Write);
Console.WriteLine();


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
