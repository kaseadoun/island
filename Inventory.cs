using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace Island
{
    public class Inventory
    {
        private List<string> _inventory;

        private string inventoryPrompt = @"._..  ..  ..___.  ..___..__..__.   ,
 | |\ |\  /[__ |\ |  |  |  |[__)\./ 
_|_| \| \/ [___| \|  |  |__||  \ |  
                                    

";

        public Inventory(List<string> inventory) {
            _inventory = inventory;
        }
        // Method to be called to display menu
        public void displayInventory() {
            Menu inventoryMenu = new Menu(inventoryPrompt, _inventory);
            if (_inventory.Count == 0) {
                _inventory.Add("You have no items.");
            }
            inventoryMenu.runInventoryMenu();
            _inventory.Remove("You have no items.");
        }
        
        // Method for different menu. Is called when the player picks the build option
        public string pickItemInventory() {
            string item;
            Menu inventoryMenu = new Menu(inventoryPrompt, _inventory);
            if (_inventory.Count == 0) {
                _inventory.Add("You have no items.");
            }
            item = inventoryMenu.runPickInventoryMenu();
            _inventory.Remove("You have no items.");
            return item;
        }
    }
}