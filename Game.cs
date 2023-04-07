using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace Island
{
    public class Game
    {
        List<string> inventory = new List<string>();
        private int _time = 600;
        public bool gamePlaying = true;
        bool homebaseCheck = true;
        bool inventoryCheck = false;
        bool pickInventoryCheck = false;
        bool mapCheck = false;
        bool buildCheck = false;
        bool shipwreck = false;
        bool jungle = false;
        bool cave = false;
        string[] homebaseMenuList = { "Homebase", "Build" };
        string[,] buildOptions = {
                {"Tree Line","[NONE]"},
                {"Grass","[NONE]"},
                {"Sand","[NONE]"},
                {"Go Back", ""}
            };
        string[] buildMenuList = { "Treeline", "Grass", "Sand" };
        string lastMenu;
        string buildPick;

        public void startMenu()
        {
            string gameName = @" ██▓  ██████  ██▓    ▄▄▄       ███▄    █ ▓█████▄ 
▓██▒▒██    ▒ ▓██▒   ▒████▄     ██ ▀█   █ ▒██▀ ██▌
▒██▒░ ▓██▄   ▒██░   ▒██  ▀█▄  ▓██  ▀█ ██▒░██   █▌
░██░  ▒   ██▒▒██░   ░██▄▄▄▄██ ▓██▒  ▐▌██▒░▓█▄   ▌
░██░▒██████▒▒░██████▒▓█   ▓██▒▒██░   ▓██░░▒████▓ 
░▓  ▒ ▒▓▒ ▒ ░░ ▒░▓  ░▒▒   ▓▒█░░ ▒░   ▒ ▒  ▒▒▓  ▒ 
 ▒ ░░ ░▒  ░ ░░ ░ ▒  ░ ▒   ▒▒ ░░ ░░   ░ ▒░ ░ ▒  ▒ 
 ▒ ░░  ░  ░    ░ ░    ░   ▒      ░   ░ ░  ░ ░  ░ 
 ░        ░      ░  ░     ░  ░         ░    ░    
                                          ░      

";
            string[] mainMenuOptions = { "Start", "Exit" };
            Menu mainMenu = new Menu(gameName, mainMenuOptions);

            int selectedOption = mainMenu.runMenu();
            switch (selectedOption)
            {
                case 0:
                    beginningDialogue();
                    break;
                case 1:
                    Environment.Exit(0);
                    break;
            }
        }

        private void beginningDialogue()
        {
            Clear();
            WriteLine("=====================================================================================");
            WriteLine("Small waves of seawater splashes on your face.");
            WriteLine("Startled by the water, you get up from the ground.");
            WriteLine("You remember your ship was caught in a storm the previous night.");
            WriteLine("Looking around, you find yourself on the beachside of an island.");
            WriteLine("But as you continue to look around, you see skulls on wooden spikes.");
            WriteLine("One look at those spikes, you understand where you are.");
            WriteLine("In a place where no cartographer should be.");
            WriteLine("Cannibal Island.");
            WriteLine("========================= Press any key to continue (1/4) ============================");
            ReadKey();
            Clear();
            WriteLine("=====================================================================================");
            WriteLine("You pat your pockets and look through your backpack.");
            WriteLine("Thankfully, you still have some items on hand.");
            WriteLine("A flare gun with one flare,");
            WriteLine("a map,");
            WriteLine("and your wristwatch.");
            WriteLine("========================= Press any key to continue (2/4) ============================");
            ReadKey();
            Clear();
            WriteLine("======================================================================================");
            WriteLine("You know what you have to do to survive.");
            WriteLine("You have to fire the flare to alert rescue, but it can only be seen at dusk.");
            WriteLine("Rescue is not immediate and shooting the flare will alert the tribe.");
            WriteLine("You must build your fortification to delay the potential death of being eaten alive\nwhile rescue is coming.");
            WriteLine("=========================== Press any key to start (3/4) =============================");
            ReadKey();
            Clear();
            WriteLine("=============================== IMPORTANT GAME TIPS ==================================");
            WriteLine("You woke up on this island at 0600. Dusk starts at 1900 and a flare will be shot then.");
            WriteLine("The cannibals only come out at night, so you're able to freely explore the island.");
            WriteLine("Every action takes time (building, going to different parts of the island, picking up\nitems) so use your time wisely.");
            WriteLine("=========================== Press any key to start (4/4) =============================");
            ReadKey();
        }

        public void runGame()
        {
            while (_time != 1900)
            {
                if (homebaseCheck)
                {
                    baseChoices();
                }
                else if (inventoryCheck)
                {
                    openInventory();
                }
                else if (pickInventoryCheck)
                {
                    openInventory();
                }
                else if (buildCheck)
                {
                    build();
                }
                else if (mapCheck)
                {
                    map();
                }
                else if (shipwreck)
                {
                    shipwreckLocation();
                }
                else if (jungle)
                {
                    jungleLocation();
                }
            }
            flareShot();
        }

        private void falseSet()
        {
            homebaseCheck = false;
            inventoryCheck = false;
            pickInventoryCheck = false;
            mapCheck = false;
            buildCheck = false;
            shipwreck = false;
            jungle = false;
            cave = false;
        }

        private void baseChoices()
        {
            string[] baseOptions = { "Map", "Inventory", "Build" };

            Menu baseChoice = new Menu($"TIME: {_time}\nYou're at your home base.", baseOptions);
            lastMenu = homebaseMenuList[0];
            int selectedOption = baseChoice.runMenu();
            switch (selectedOption)
            {
                case 0:
                    falseSet();
                    mapCheck = true;
                    break;
                case 1:
                    falseSet();
                    inventoryCheck = true;
                    break;
                case 2:
                    falseSet();
                    buildCheck = true;
                    break;
            }
        }

        private void openInventory()
        {
            Inventory itemMenu = new Inventory(inventory);
            if (lastMenu == homebaseMenuList[0])
            {
                falseSet();
                itemMenu.displayInventory();
                homebaseCheck = true;
            }
            else if (lastMenu == homebaseMenuList[1])
            {
                falseSet();
                string item = itemMenu.pickItemInventory();
                if (buildPick == buildMenuList[0])
                {
                    switch (item)
                    {
                        case "You have no items.":
                            WriteLine("You can't build with no items.");
                            break;
                        default:
                            buildOptions[0, 1] = item;
                            break;
                    }
                }
                else if (buildPick == buildMenuList[1])
                {
                    switch (item)
                    {
                        case "You have no items.":
                            WriteLine("You can't build with no items.");
                            break;
                        default:
                            buildOptions[1, 1] = item;
                            break;
                    }
                }
                else if (buildPick == buildMenuList[2])
                {
                    switch (item)
                    {
                        case "You have no items.":
                            WriteLine("You can't build with no items.");
                            break;
                        default:
                            buildOptions[2, 1] = item;
                            break;
                    }
                }
                buildCheck = true;
            }
        }

        private void build()
        {
            Menu buildMenu = new Menu($"TIME: {_time}\nWhat would you like to fortify?", buildOptions);
            lastMenu = homebaseMenuList[1];
            int selectedOption = buildMenu.runBuildMenu();
            switch (selectedOption)
            {
                case 0:
                    falseSet();
                    buildPick = buildMenuList[0];
                    pickInventoryCheck = true;
                    break;
                case 1:
                    falseSet();
                    buildPick = buildMenuList[1];
                    pickInventoryCheck = true;
                    break;
                case 2:
                    falseSet();
                    buildPick = buildMenuList[2];
                    pickInventoryCheck = true;
                    break;
                case 3:
                    falseSet();
                    homebaseCheck = true;
                    break;
            }
        }

        private void map()
        {
            Dictionary<string, int> Locations = new Dictionary<string, int>();
            Locations.Add("Shipwreck", 30);
            Locations.Add("Jungle", 45);
            Locations.Add("Cave", 60);
            Locations.Add("Back", 0);
            Menu locationMenu = new Menu($"TIME: {_time}\nWhere would you like to go?", Locations);
            int selectedOption = locationMenu.runLocationMenu();
            switch (selectedOption)
            {
                case 0:
                    timeAdd(30);
                    falseSet();
                    shipwreck = true;
                    break;
                case 1:
                    timeAdd(45);
                    falseSet();
                    jungle = true;
                    break;
                case 2:
                    timeAdd(60);
                    falseSet();
                    cave = true;
                    break;
                case 3:
                    falseSet();
                    homebaseCheck = true;
                    break;
            }
        }

        private void shipwreckLocation()
        {
            Dictionary<string, int> Explore = new Dictionary<string, int>();
            Explore.Add("Search the Lower Part of the Boat", 30);
            Explore.Add("Search the Upper Part of the Boat", 30);
            Explore.Add("Go Back to Your Homebase", 30);
            Menu exploreShip = new Menu($"TIME: {_time}\nYou find a shipwreck. The ship has been cut clean in half. It looks like this ship has\nbeen here for awhile. Maybe there's some stuff you can use.", Explore);
            int selectedOption = exploreShip.runLocationMenu();
            Clear();
            switch (selectedOption)
            {
                case 0:
                    timeAdd(30);
                    if (!inventory.Contains("Axe"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You search the lower part of the ship. You found an axe.");
                        WriteLine("========================================================================================");
                        inventory.Add("Axe");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("Nothing else was found.");
                        WriteLine("========================================================================================");
                    }
                    ReadKey();
                    falseSet();
                    shipwreck = true;
                    break;
                case 1:
                    timeAdd(30);
                    if (inventory.Contains("Rope"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You got up to the upper part of the ship. You found an old pistol with one bullet. Looks\nlike it still works.");
                        WriteLine("========================================================================================");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You try to find somewhere to get to the upper deck, but there is no way up. You're\ngoing to need something to get up.");
                        WriteLine("========================================================================================");
                    }
                    ReadKey();
                    falseSet();
                    shipwreck = true;
                    break;
                case 2:
                    timeAdd(30);
                    falseSet();
                    homebaseCheck = true;
                    break;
            }
        }

        private void jungleLocation()
        {
            Dictionary<string, int> Explore = new Dictionary<string, int>();
            Explore.Add("Cut the tree.", 90);
            Explore.Add("Search Around the Jungle", 120);
            Explore.Add("Go Back to Your Homebase", 45);
            Menu exploreJungle = new Menu($"TIME: {_time}\nYou head into the jungle. You see one tree that looks like it can be cut easily and\n can be used for your fortification.", Explore);
            int selectedOption = exploreJungle.runLocationMenu();
            Clear();
            switch (selectedOption)
            {
                case 0:
                    timeAdd(90);
                    if (inventory.Contains("Axe"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You cut the tree down and chop it up into bits you can carry.");
                        WriteLine("========================================================================================");
                        inventory.Add("Pile of Wood");
                    }
                    else if (inventory.Contains("Pile of Wood"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You try to cut down another tree. Unfortunately, your axe is too blunt to cut it down in\time. So you give up.");
                        WriteLine("========================================================================================");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You try to cut down the tree with your bare hands. Unfortunate, this isn't Minecraft.\nEven worse, you tried for 90 minutes.");
                        WriteLine("========================================================================================");
                    }
                    ReadKey();
                    falseSet();
                    jungle = true;
                    break;
                case 1:
                    timeAdd(120);
                    if (inventory.Contains("Rope"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You explore the jungle again. It took you so long to find nothing.");
                        WriteLine("========================================================================================");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You search the whole jungle. The only thing you were able to find was a rope. This seems\nlike it can get you to high places.");
                        WriteLine("========================================================================================");
                        inventory.Add("Rope");
                    }
                    ReadKey();
                    falseSet();
                    jungle = true;
                    break;
                case 2:
                    timeAdd(45);
                    ReadKey();
                    falseSet();
                    homebaseCheck = true;
                    break;
            }
        }

        // private void caveLocation () {

        // }

        private void timeAdd(int time)
        {   
            int minutes = _time % 100;
            int minutesTillHour = 60 - minutes;
            if (minutes % 100 == 60) {
                _time -= 60;
                _time += 100;
            }
            if (minutes + time >= 60 ) {
                _time -= minutes;
                _time += 100;
                time -= minutesTillHour;
            }
            while (time >= 60) {
                _time += 100;
                time -= 60;
            }
            _time += time;
        }

        private void flareShot()
        {
            Clear();
            WriteLine("You shoot the flare.");
            gamePlaying = false;
        }

        private void cannibalSequence()
        {

        }
    }
}