using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace Island
{
    public class Menu
    {
        private int _selectedIndex;
        private string[] _options;
        private List<string> _inventoryOptions;
        private string[,] _buildOptions;
        private Dictionary<string, int> _location;
        private string _prompt;

        // Constructor for choices
        public Menu(string prompt, string[] options)
        {
            _prompt = prompt;
            _options = options;
            _selectedIndex = 0;
        }

        // Overloaded constructor specifically for the inventory menu
        public Menu(string prompt, List<string> inventoryOptions)
        {
            _prompt = prompt;
            _inventoryOptions = inventoryOptions;
            _selectedIndex = 0;
        }

        // Overloaded constructor specifically for the build menu
        public Menu(string prompt, string[,] buildOptions)
        {
            _prompt = prompt;
            _buildOptions = buildOptions;
            _selectedIndex = 0;
        }

        public Menu(string prompt, Dictionary<string, int> locations)
        {
            _prompt = prompt;
            _location = locations;
            _selectedIndex = 0;
        }

        // Method to display player options
        private void displayOptions()
        {
            for (int i = 0; i < _options.Length; i++)
            {
                string currentOption = _options[i];
                string choice;

                if (i == _selectedIndex)
                {
                    choice = ">>";
                }
                else
                {
                    choice = "  ";
                }

                WriteLine($"{choice} {currentOption}");
            }
        }

        // Method to display inventory options
        private void displayInventory()
        {
            for (int i = 0; i < _inventoryOptions.Count; i++)
            {
                string currentItem = _inventoryOptions[i];

                WriteLine($"  {currentItem}");
            }
        }
        // Method to display inventory with picking option
        private void displayPickInventory()
        {
            for (int i = 0; i < _inventoryOptions.Count; i++)
            {
                string currentItem = _inventoryOptions[i];
                string choice;

                if (i == _selectedIndex)
                {
                    choice = ">>";
                }
                else
                {
                    choice = "  ";
                }

                WriteLine($"{choice} {currentItem}");
            }
        }
        // Display build menu options
        private void displayBuild()
        {
            for (int i = 0; i < _buildOptions.Length / 2; i++)
            {
                string currentItem = $"{_buildOptions[i, 0]}  {_buildOptions[i, 1]}";
                string choice;

                if (i == _selectedIndex)
                {
                    choice = ">>";
                }
                else
                {
                    choice = "  ";
                }

                WriteLine($"{choice} {currentItem}");
            }
        }
        // Display location menu options
        private void displayLocation()
        {
            for (int i = 0; i < _location.Count; i++)
            {
                string currentItem = $"{_location.ElementAt(i).Key} : {_location.ElementAt(i).Value}";
                string choice;

                if (i == _selectedIndex)
                {
                    choice = ">>";
                }
                else
                {
                    choice = "  ";
                }

                WriteLine($"{choice} {currentItem} minutes");
            }
        }

        // Public method to allow the player to use arrow keys for the choices
        public int runMenu()
        {
            ConsoleKey pressedKey;
            do
            {
                Clear();
                WriteLine(_prompt);
                WriteLine("================Use the arrow keys to cycle through your the options.==================");
                displayOptions();
                WriteLine("===============================Press enter to decide.==================================");
                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow && _selectedIndex != 0)
                {
                    _selectedIndex--;
                }
                else if (pressedKey == ConsoleKey.DownArrow && _selectedIndex != _options.Length - 1)
                {
                    _selectedIndex++;
                }
            } while (pressedKey != ConsoleKey.Enter);

            return _selectedIndex;
        }
        // Run inventory method
        public void runInventoryMenu()
        {
            ConsoleKey pressedKey;
            do
            {
                Clear();
                WriteLine(_prompt);
                WriteLine("==============================Items you currently have.================================");
                displayInventory();
                WriteLine("===============================Press enter to decide.==================================");
                pressedKey = Console.ReadKey(true).Key;
            } while (pressedKey != ConsoleKey.Enter);
        }
        // Run inventory method that allows the player to pick and choose with arrow keys
        public string runPickInventoryMenu()
        {
            ConsoleKey pressedKey;
            do
            {
                Clear();
                WriteLine(_prompt);
                WriteLine("================Use the arrow keys to cycle through your the options.==================");
                displayPickInventory();
                WriteLine("===============================Press enter to decide.==================================");
                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow && _selectedIndex != 0)
                {
                    _selectedIndex--;
                }
                else if (pressedKey == ConsoleKey.DownArrow && _selectedIndex != _inventoryOptions.Count - 1)
                {
                    _selectedIndex++;
                }
            } while (pressedKey != ConsoleKey.Enter);

            return _inventoryOptions[_selectedIndex];
        }
        // Run build menu method that allows the player to pick and choose with arrow keys
        public int runBuildMenu()
        {
            ConsoleKey pressedKey;
            do
            {
                Clear();
                WriteLine(_prompt);
                WriteLine("=======================Pick the location you want to fortify.==========================");
                displayBuild();
                WriteLine("===============================Press enter to decide.==================================");
                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow && _selectedIndex != 0)
                {
                    _selectedIndex--;
                }
                else if (pressedKey == ConsoleKey.DownArrow && _selectedIndex != _buildOptions.Length / 2 - 1)
                {
                    _selectedIndex++;
                }
            } while (pressedKey != ConsoleKey.Enter);

            return _selectedIndex;
        }
        // Run map menu method that allows the player to pick and choose with arrow keys

        public int runLocationMenu()
        {
            ConsoleKey pressedKey;
            do
            {
                Clear();
                WriteLine(_prompt);
                WriteLine("========================================================================================");
                WriteLine("Pick where you want to go. (The number shows how many minutes it will take to go there.)");
                WriteLine("========================================================================================");
                displayLocation();
                WriteLine("================================Press enter to decide.==================================");
                pressedKey = Console.ReadKey(true).Key;
                if (pressedKey == ConsoleKey.UpArrow && _selectedIndex != 0)
                {
                    _selectedIndex--;
                }
                else if (pressedKey == ConsoleKey.DownArrow && _selectedIndex != _location.Count - 1)
                {
                    _selectedIndex++;
                }
            } while (pressedKey != ConsoleKey.Enter);

            return _selectedIndex;
        }
    }
}