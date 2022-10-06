using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuSystem
{
    public class Menu
    {
        private readonly EMenuLevel _menuLevel;


        public void Move<TMenuItem>(List<TMenuItem> list, int oldIndex, int newIndex)
        {
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
        }

        public MenuSelectionState[] GetItems(int i)
        {
            return CopyOfItems(MenuItems);
        }

        private MenuSelectionState[] CopyOfItems(List<MenuItem> menuItems)
        {
            var res = new MenuSelectionState[menuItems.Count];
            return res;
        }


        public List<MenuItem> MenuItems = new();
        private readonly MenuItem _menuItemExit = new("Exit", null);
        private readonly MenuItem _menuItemReturn = new("Return", null);
        private readonly MenuItem _menuItemMain = new("Main", null);


        private readonly HashSet<string> _menuShortCuts = new();
        private readonly HashSet<string> _menuSpecialShortCuts = new();

        private readonly string _title;
        private readonly Func<string>? _getHeaderInfoString;

        public Menu(Func<string> getHeaderInfoString, string title, EMenuLevel menuLevel)
        {
            _getHeaderInfoString = getHeaderInfoString;
            _title = title;
            _menuLevel = menuLevel;

            switch (_menuLevel)
            {
                case EMenuLevel.Root:
                    _menuSpecialShortCuts.Add(_menuItemExit.Title);
                    break;
                case EMenuLevel.First:
                    _menuSpecialShortCuts.Add(_menuItemReturn.Title);
                    _menuSpecialShortCuts.Add(_menuItemMain.Title);
                    _menuSpecialShortCuts.Add(_menuItemExit.Title);
                    break;
                case EMenuLevel.SecondOrMore:
                    _menuSpecialShortCuts.Add(_menuItemReturn.Title);
                    _menuSpecialShortCuts.Add(_menuItemMain.Title);
                    _menuSpecialShortCuts.Add(_menuItemExit.Title);
                    break;
            }
        }


        public Menu(string title, EMenuLevel menuLevel)
        {
            _title = title;
            _menuLevel = menuLevel;

            switch (_menuLevel)
            {
                case EMenuLevel.Root:
                    _menuSpecialShortCuts.Add(_menuItemExit.Title);
                    break;
                case EMenuLevel.First:
                    _menuSpecialShortCuts.Add(_menuItemReturn.Title);
                    _menuSpecialShortCuts.Add(_menuItemMain.Title);
                    _menuSpecialShortCuts.Add(_menuItemExit.Title);
                    break;
                case EMenuLevel.SecondOrMore:
                    _menuSpecialShortCuts.Add(_menuItemReturn.Title);
                    _menuSpecialShortCuts.Add(_menuItemMain.Title);
                    _menuSpecialShortCuts.Add(_menuItemExit.Title);
                    break;
            }
        }


        public void AddMenuItem(MenuItem item, int position = -1)
        {
            if (_menuSpecialShortCuts.Add(item.Title) == false)
                throw new ApplicationException($"Conflicting menu shortcut {item.Title}");
            if (_menuShortCuts.Add(item.Title) == false)
                throw new ApplicationException($"Conflicting menu shortcut {item.Title}");


            if (position == -1)
                MenuItems.Add(item);
            else
                MenuItems.Insert(position, item);
        }

        public void DeleteMenuItem(int position = 0)
        {
            MenuItems.RemoveAt(position);
        }

        public void AddMenuItems(List<MenuItem> items)
        {
            foreach (var menuItem in items) AddMenuItem(menuItem);
        }


        public MenuSelectionState[] MenuOptions = Array.Empty<MenuSelectionState>();

        public string Run()
        {
            switch (_menuLevel)
            {
                case EMenuLevel.Root:
                    MenuItems.Add(_menuItemExit);
                    break;
                case EMenuLevel.First:
                    MenuItems.Add(_menuItemReturn);
                    MenuItems.Add(_menuItemExit);
                    break;
                case EMenuLevel.SecondOrMore:
                    MenuItems.Add(_menuItemReturn);
                    MenuItems.Add(_menuItemExit);
                    MenuItems.Add(_menuItemMain);
                    break;
            }

            var selected = false;
            var runDone = false;

            for (var i = 0; i < MenuItems.Count; i++) MenuOptions = GetItems(i);

            MenuOptions[0].IsSelected = true;

            var currentPostition = 0;
            var userChoice = "";

            do
            {
                do
                {
                    OutputMenu();

                    var keyInfo = Console.ReadKey();
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (currentPostition < MenuItems.Count - 1)
                            {
                                MenuOptions[currentPostition].IsSelected = false;
                                currentPostition += 1;
                                MenuOptions[currentPostition].IsSelected = true;
                            }

                            break;
                        case ConsoleKey.UpArrow:
                            if (currentPostition > 0)
                            {
                                MenuOptions[currentPostition].IsSelected = false;
                                currentPostition -= 1;
                                MenuOptions[currentPostition].IsSelected = true;
                            }

                            break;
                        case ConsoleKey.Enter:
                            for (var i = 0; i < MenuItems.Count; i++)
                                if (MenuOptions[currentPostition].IsSelected == true)
                                    userChoice = MenuItems.ElementAt(currentPostition).Title;

                            selected = true;
                            break;
                    }
                } while (!selected);

                OutputMenu();
                var isInputValid = _menuShortCuts.Contains(userChoice);
                if (isInputValid)
                {
                    var item = MenuItems.FirstOrDefault(t => t.Title == userChoice);
                    userChoice = item?.RunMethod == null ? userChoice : item.RunMethod();
                }

                runDone = _menuSpecialShortCuts.Contains(userChoice);

                if (!runDone && !isInputValid) Console.WriteLine($"Unknown shortcut '{userChoice}'!");
            } while (!runDone);

            if (userChoice == _menuItemReturn.Title) return "";

            return userChoice;
        }

        private void OutputMenu()
        {
            Console.Clear();
            Console.WriteLine("====> " + _title + " <====");
            if (_getHeaderInfoString != null)
            {
                var headerInfo = _getHeaderInfoString();
                if (headerInfo != null) Console.WriteLine(headerInfo);
            }

            Console.WriteLine("-------------------");


            for (var i = 0; i < MenuItems.Count; i++)
                if (MenuOptions[i].IsSelected == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (_menuLevel == EMenuLevel.Root && MenuItems.ElementAt(i).Title == "Exit" ||
                        _menuLevel == EMenuLevel.First && MenuItems.ElementAt(i).Title == "Return" ||
                        _menuLevel == EMenuLevel.SecondOrMore && MenuItems.ElementAt(i).Title == "Return")
                    {
                        Console.ResetColor();
                        Console.WriteLine("-------------------");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(MenuItems.ElementAt(i).Title);
                    }
                    else
                    {
                        Console.WriteLine(MenuItems.ElementAt(i).Title);
                    }

                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (_menuLevel == EMenuLevel.Root && MenuItems.ElementAt(i).Title == "Exit" ||
                        _menuLevel == EMenuLevel.First && MenuItems.ElementAt(i).Title == "Return" ||
                        _menuLevel == EMenuLevel.SecondOrMore && MenuItems.ElementAt(i).Title == "Return")
                    {
                        Console.WriteLine("-------------------");
                        Console.WriteLine(MenuItems.ElementAt(i).Title);
                    }
                    else
                    {
                        Console.WriteLine(MenuItems.ElementAt(i).Title);
                    }
                }
        }
    }
}