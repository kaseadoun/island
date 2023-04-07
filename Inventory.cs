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

        public void displayInventory() {
            Menu inventoryMenu = new Menu(inventoryPrompt, _inventory);
            if (_inventory.Count == 0) {
                _inventory.Add("You have no items.");
            }
            inventoryMenu.runInventoryMenu();
            _inventory.Remove("You have no items.");
        }
        
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