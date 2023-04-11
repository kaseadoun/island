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
        // List created for the inventory
        List<string> inventory = new List<string>();
        // Starting time for the game
        private int _time = 600;
        // Checks if game is being played
        public bool gamePlaying = true;
        private bool shotFlare = false;
        // Checks which menu the player is in
        private bool homebaseCheck = true;
        private bool inventoryCheck = false;
        private bool pickInventoryCheck = false;
        private bool mapCheck = false;
        private bool buildCheck = false;
        private bool shipwreck = false;
        private bool jungle = false;
        private bool cave = false;
        private string[] homebaseMenuList = { "Homebase", "Build" };
        // Multidimensional Array of locations to fortify containing the Location and item used to fortify the Location
        private string[,] buildOptions = {
                {"Tree Line","[NONE]"},
                {"Grass","[NONE]"},
                {"Sand","[NONE]"},
                {"Go Back", ""}
            };
        // Single Array of locations for the Map Menu
        private string[] buildMenuList = { "Treeline", "Grass", "Sand" };
        // Reference to last Menu the player wa in
        private string lastMenu;
        // Reference to last Build Menu option picked
        private string buildPick;
        // Checks if area is explored
        private bool shipwreckLowerDeck = false;
        private bool shipwreckUpperDeck = false;
        private bool jungleTreeCut = false;
        private bool jungleExplored = false;
        private bool caveExplored = false;
        private bool caveMilitaryExplored = false;

        // Title Screen for the game
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

        // Beginning dialogue for the player to understand the premise and reason for the game
        private static void beginningDialogue()
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

        // While the game is running, checks the time and will continuously check which menu the user is in under a certain time
        public void runGame()
        {
            while (_time < 1900)
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
                else if (cave)
                {
                    caveLocation();
                }
            }
            flareShot();
        }

        // Method to set every menu option as false
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

        // Choices when in base
        private void baseChoices()
        {
            string[] baseOptions = { "Map", "Inventory", "Build", "Fire Flare" };

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
                case 3:
                    falseSet();
                    flareShot();
                    break;
            }
        }

        // Opens inventory
        private void openInventory()
        {
            Inventory itemMenu = new Inventory(inventory);
            string addBackItem;
            // If last menu is inventory, then a certain inventory will display
            if (lastMenu == homebaseMenuList[0])
            {
                falseSet();
                itemMenu.displayInventory();
                homebaseCheck = true;
            }
            // If last menu is build menu, then the inventory that allows the character to pick will open
            else if (lastMenu == homebaseMenuList[1])
            {
                falseSet();
                string item = itemMenu.pickItemInventory();
                // If Build location is Treeline
                if (buildPick == buildMenuList[0])
                {
                    switch (item)
                    {
                        case "You have no items.":
                            WriteLine("You can't build with no items.");
                            break;
                        default:
                            Clear();
                            if (buildOptions[0, 1] == "[NONE]")
                            {
                                buildOptions[0, 1] = item;
                                inventory.Remove(item);
                            }
                            else
                            {
                                addBackItem = buildOptions[0, 1];
                                buildOptions[0, 1] = item;
                                inventory.Add(addBackItem);
                                inventory.Remove(item);
                            }
                            if (item == "Axe" || item == "Gun")
                            {
                                timeAdd(5);
                                WriteLine("========================================================================================");
                                WriteLine($"You try to set up the {item} in the {buildOptions[0, 0]}.\nIt took you 5 minutes.\nJust to have the item lay in the grass.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            else if (item == "Rope")
                            {
                                timeAdd(20);
                                WriteLine("========================================================================================");
                                WriteLine($"You set up the {item} in the {buildOptions[0, 0]}.\nIt took you 20 minutes.\nHopefully the {item} will delay their attacks.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            else if (item == "Wood Spikes" || item == "Spool of Barbed Wire" || item == "Dynamite and Detonator")
                            {
                                timeAdd(50);
                                WriteLine("========================================================================================");
                                WriteLine($"You set up the {item} in the {buildOptions[0, 0]}.\nIt took you 50 minutes.\nThis will definitely delay them. Hopefully it's the most effective method.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            ReadKey();
                            break;
                    }
                }
                // If Build location is Grass
                else if (buildPick == buildMenuList[1])
                {
                    switch (item)
                    {
                        case "You have no items.":
                            WriteLine("You can't build with no items.");
                            break;
                        default:
                            Clear();
                            if (buildOptions[1, 1] == "[NONE]")
                            {
                                buildOptions[1, 1] = item;
                                inventory.Remove(item);
                            }
                            else
                            {
                                addBackItem = buildOptions[1, 1];
                                buildOptions[1, 1] = item;
                                inventory.Add(addBackItem);
                                inventory.Remove(item);
                            }
                            if (item == "Axe" || item == "Gun" || item == "Rope")
                            {
                                timeAdd(5);
                                WriteLine("========================================================================================");
                                WriteLine($"You try to set up the {item} in the {buildOptions[1, 0]}.\nIt took you 5 minutes.\nJust to have the item lay in the grass.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            else if (item == "Wood Spikes" || item == "Spool of Barbed Wire" || item == "Dynamite and Detonator")
                            {
                                timeAdd(50);
                                WriteLine("========================================================================================");
                                WriteLine($"You set up the {item} in the {buildOptions[1, 0]}.\nIt took you 50 minutes.\nThis will definitely delay them. Hopefully it's the most effective method.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            ReadKey();
                            break;
                    }
                }
                // If Build location is Sand
                else if (buildPick == buildMenuList[2])
                {
                    switch (item)
                    {
                        case "You have no items.":
                            WriteLine("You can't build with no items.");
                            break;
                        default:
                            Clear();
                            if (buildOptions[2, 1] == "[NONE]")
                            {
                                buildOptions[2, 1] = item;
                                inventory.Remove(item);
                            }
                            else
                            {
                                addBackItem = buildOptions[2, 1];
                                buildOptions[2, 1] = item;
                                inventory.Add(addBackItem);
                                inventory.Remove(item);
                            }
                            if (item == "Axe" || item == "Gun" || item == "Rope")
                            {
                                timeAdd(5);
                                WriteLine("========================================================================================");
                                WriteLine($"You try to set up the {item} in the {buildOptions[2, 0]}.\nIt took you 5 minutes.\nJust to have the item lay in the sand.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            else if (item == "Wood Spikes" || item == "Spool of Barbed Wire" || item == "Dynamite and Detonator")
                            {
                                timeAdd(50);
                                WriteLine("========================================================================================");
                                WriteLine($"You set up the {item} in the {buildOptions[2, 0]}.\nIt took you 50 minutes.\nThis will definitely delay them. Hopefully it's the most effective method.");
                                WriteLine("============================ Press Any Key to Continue =================================");
                            }
                            ReadKey();
                            break;
                    }
                }
                buildCheck = true;
            }
        }

        // Build Menu
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

        // Map Menu
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

        // Shipwreck Menu
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
                    if (shipwreckLowerDeck == false)
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You search the lower part of the ship. You found an axe.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                        shipwreckLowerDeck = true;
                        inventory.Add("Axe");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("Nothing else was found.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    ReadKey();
                    falseSet();
                    shipwreck = true;
                    break;
                case 1:
                    timeAdd(30);
                    if (inventory.Contains("Rope") && shipwreckUpperDeck == false)
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You got up to the upper part of the ship. You found an old pistol with one bullet. Looks\nlike it still works.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                        shipwreckUpperDeck = true;
                        inventory.Add("Gun");
                    }
                    else if (!inventory.Contains("Rope"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You try to find somewhere to get to the upper deck, but there is no way up. You're\ngoing to need something to get up.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    else if (inventory.Contains("Rope") && shipwreckUpperDeck == true) {
                        WriteLine("========================================================================================");
                        WriteLine("You got up to the upper part of the ship. You find nothing else.");
                        WriteLine("============================ Press Any Key to Continue =================================");
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

        // Jungle Menu
        private void jungleLocation()
        {
            Dictionary<string, int> Explore = new Dictionary<string, int>();
            Explore.Add("Cut the tree.", 60);
            Explore.Add("Search Around the Jungle", 30);
            Explore.Add("Go Back to Your Homebase", 45);
            Menu exploreJungle = new Menu($"TIME: {_time}\nYou head into the jungle. You see one tree that looks like it can be cut easily and\n can be used for your fortification.", Explore);
            int selectedOption = exploreJungle.runLocationMenu();
            Clear();
            switch (selectedOption)
            {
                case 0:
                    timeAdd(60);
                    if (jungleTreeCut == false && inventory.Contains("Axe"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You cut the tree down and chop it up into bits you're using for your fortification. You\nobtained Wood Spikes.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                        jungleTreeCut = true;
                        inventory.Add("Wood Spikes");
                    }
                    else if (jungleTreeCut == true)
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You try to cut down another tree. Unfortunately, your axe is too blunt to cut it down in\ntime. So you give up.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    else if (jungleTreeCut == false && !inventory.Contains("Axe"))
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You try to cut down the tree with your bare hands. Unfortunate, this isn't Minecraft.\nEven worse, you tried for 60 minutes.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    ReadKey();
                    falseSet();
                    jungle = true;
                    break;
                case 1:
                    timeAdd(30);
                    if (jungleExplored == true)
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You explore the jungle again. It took you so long to find nothing.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You search the whole jungle. The only thing you were able to find was a rope. This seems\nlike it can get you to high places or can be used as a trap.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                        jungleExplored = true;
                        inventory.Add("Rope");
                    }
                    ReadKey();
                    falseSet();
                    jungle = true;
                    break;
                case 2:
                    timeAdd(45);
                    falseSet();
                    homebaseCheck = true;
                    break;
            }
        }

        // Cave Menu
        private void caveLocation()
        {
            Dictionary<string, int> Explore = new Dictionary<string, int>();
            Explore.Add("Explore the Cave", 45);
            Explore.Add("Search the Abandoned Military Base", 45);
            Explore.Add("Go Back to Your Homebase", 60);
            Menu exploreCave = new Menu($"TIME: {_time}\nYou go into the cave. The cave looks like it was manmade and\nwas used a base for the Japanese Army in World War II. Maybe you'll\n be able to find something useful.", Explore);
            int selectedOption = exploreCave.runLocationMenu();
            Clear();
            switch (selectedOption)
            {
                case 0:
                    timeAdd(45);
                    if (caveExplored == true)
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You go deep into the cave again. No other items were found.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You go deep into the cave. Looks like this cave was being mined using dynamite and a\ndetonator. This should be useful.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                        caveExplored = true;
                        inventory.Add("Dynamite and Detonator");
                    }
                    ReadKey();
                    falseSet();
                    cave = true;
                    break;
                case 1:
                    timeAdd(45);
                    if (caveMilitaryExplored == true)
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You scour through the abandoned military supplies again. Nothing really useful left.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                    }
                    else
                    {
                        WriteLine("========================================================================================");
                        WriteLine("You scour through the abandoned military supplies.\nYou find a Spool of Barbed Wire.");
                        WriteLine("============================ Press Any Key to Continue =================================");
                        caveMilitaryExplored = true;
                        inventory.Add("Spool of Barbed Wire");
                    }
                    ReadKey();
                    falseSet();
                    cave = true;
                    break;
                case 2:
                    timeAdd(60);
                    falseSet();
                    homebaseCheck = true;
                    break;
            }
        }

        // Time Calculation Method
        private void timeAdd(int time)
        {
            int minutes = _time % 100;
            int minutesTillHour = 60 - minutes;
            if (minutes % 100 == 60)
            {
                _time -= 60;
                _time += 100;
            }
            if (minutes + time >= 60)
            {
                _time -= minutes;
                _time += 100;
                time -= minutesTillHour;
            }
            while (time >= 60)
            {
                _time += 100;
                time -= 60;
            }
            _time += time;
        }
        // Method for the flare shooting action
        private void flareShot()
        {
            Clear();
            if (_time < 1800)
            {
                WriteLine("========================================================================================");
                WriteLine("You shoot the flare too early.");
                WriteLine("Despite your efforts, no one will come rescue you.");
                WriteLine("As it gets dark, you become consumed by the islanders.");
                WriteLine("========================================================================================");
                ReadKey();
                endGame();
            }
            else if (_time >= 1800 && _time < 1900)
            {
                WriteLine("========================================================================================");
                WriteLine("You wait until 1900.");
                WriteLine("You shoot the flare.");
                WriteLine("They start coming.");
                WriteLine("====================== Press Any Key to Start the Cannibal Hunt ========================");
                ReadKey();
                cannibalSequence();
            }
            else if (_time >= 1900)
            {
                WriteLine("========================================================================================");
                WriteLine("The time is up.");
                WriteLine("You get back to your homebase and shoot the flare.");
                WriteLine("They start coming.");
                WriteLine("====================== Press Any Key to Start the Cannibal Hunt ========================");
                ReadKey();
                cannibalSequence();
            }
        }
        // Game Ending Sequence
        private void cannibalSequence()
        {
            Clear();
            int cannibals = 150;
            string first = buildOptions[0, 1];
            string second = buildOptions[1, 1];
            string third = buildOptions[2, 1];
            string result = @"   ▄████████    ▄████████    ▄████████ ███    █▄   ▄█           ███     
  ███    ███   ███    ███   ███    ███ ███    ███ ███       ▀█████████▄ 
  ███    ███   ███    █▀    ███    █▀  ███    ███ ███          ▀███▀▀██ 
 ▄███▄▄▄▄██▀  ▄███▄▄▄       ███        ███    ███ ███           ███   ▀ 
▀▀███▀▀▀▀▀   ▀▀███▀▀▀     ▀███████████ ███    ███ ███           ███     
▀███████████   ███    █▄           ███ ███    ███ ███           ███     
  ███    ███   ███    ███    ▄█    ███ ███    ███ ███▌    ▄     ███     
  ███    ███   ██████████  ▄████████▀  ████████▀  █████▄▄██    ▄████▀   
  ███    ███                                      ▀                     

";

            WriteLine($"They are approaching from the treeline.\n >>> {cannibals} Cannibals Charge In <<<");
            Thread.Sleep(3000);
            // First Sequence - Treeline
            if (first == "Axe" || first == "Gun with On")
            {
                WriteLine($"The {first} did nothing to the cannibals. The {first} just laid there and the cannibals just ran past it.");
            }
            else if (first == "Rope")
            {
                cannibals -= 5;
                WriteLine($"The {first} tripped some cannibals and scared some of them off.\n >>> {cannibals} Cannibals Remain <<<");
            }
            else if (first == "Spool of Barbed Wire" || first == "Wood Spikes")
            {
                cannibals -= 30;
                WriteLine($"The {first} cut and stabbed some cannibals. The injuries were enough to scare a bunch of them off. \n >>> {cannibals} Cannibals Remain <<<");
            }
            else if (first == "Dynamite and Detonator")
            {
                cannibals -= 90;
                WriteLine($"You used the {first}. The explosion killed a large amount of cannibals. The trees surrounding start burning\nand set some of them aflame. Some trees fell over some of the savages.\n >>> {cannibals} Cannibals Remain <<<");
            }
            else
            {
                WriteLine($"You made no effort into putting fortifications in the Treeline.\n >>> {cannibals} Cannibals Remain <<<");
            }
            Thread.Sleep(5000);
            // Second Sequence - Grass
            if (second == "Axe" || second == "Gun" || second == "Rope")
            {
                WriteLine($"The {second} did nothing to the cannibals. The {second} just laid there and the cannibals just ran past it.");
            }
            else if (second == "Spool of Barbed Wire" || second == "Wood Spikes")
            {
                cannibals -= 30;
                WriteLine($"The {second} cut and stabbed some cannibals. The injuries were enough to scare a bunch of them off. \n >>> {cannibals} Cannibals Remain <<<");
            }
            else if (second == "Dynamite and Detonator")
            {
                cannibals -= 55;
                WriteLine($"You used the {second}. The explosion killed a large amount of cannibals.\n >>> {cannibals} Cannibals Remain <<<");
            }
            else
            {
                WriteLine($"You made no effort into putting fortifications in the Grass.\n >>> {cannibals} Cannibals Remain <<<");
            }
            Thread.Sleep(5000);
            // Third Sequence - Sand
            if (third == "Axe" || third == "Gun" || third == "Rope")
            {
                WriteLine($"The {third} did nothing to the cannibals. The {third} just laid there and the cannibals just ran past it.");
            }
            else if (third == "Spool of Barbed Wire")
            {
                if (first == "Dynamite and Detonator" && second == "Wood Spikes")
                {
                    cannibals -= 29;
                    WriteLine($"The {third} cut and stabbed some cannibals. The injuries were enough to scare the rest of them...");
                    Thread.Sleep(5000);
                    WriteLine($"Except 1.");
                }
                else
                {
                    cannibals -= 30;
                    WriteLine($"The {third} cut and stabbed some cannibals. The injuries were enough to scare a bunch of them off. \n >>> {cannibals} Cannibals Remain <<<");
                }
            }
            else if (third == "Wood Spikes")
            {
                cannibals -= 10;
                WriteLine($"The {third} slumps over on the sand as they run towards you. Only some were injured from the spikes while\nthe others ran or hopped over it. >>> {cannibals} Cannibals Remain <<<");
            }
            else if (third == "Dynamite and Detonator")
            {
                cannibals -= 55;
                WriteLine($"You used the {third}. The explosion killed a large amount of cannibals.\n >>> {cannibals} Cannibals Remain <<<");
            }
            else
            {
                WriteLine($"You made no effort into putting fortifications in the Grass.\n >>> {cannibals} Cannibals Remain <<<");
            }
            Thread.Sleep(5000);
            Clear();
            WriteLine(result);
            Thread.Sleep(3000);
            if (cannibals != 1)
            {
                WriteLine($"Without anymore fortification, the remaining {cannibals} charge.");
                Thread.Sleep(4000);
                if (inventory.Contains("Gun"))
                {
                    cannibals -= 6;
                    WriteLine($"You fire the gun with one bullet, killing 1 cannibal and scaring a couple of them off from the\nsound of the firearm.\n{cannibals} Cannibals Remain");
                    Thread.Sleep(4000);
                    if (inventory.Contains("Axe"))
                    {
                        cannibals -= 1;
                        WriteLine("Without anymore bullets, you take out your axe and hysterically swing it knocking out 1 cannibal\ncausing the other cannibals to step back for a moment to decide their next move.");
                        Thread.Sleep(4000);
                    }
                }
                else if (!inventory.Contains("Gun"))
                {
                    if (inventory.Contains("Axe"))
                    {
                        cannibals -= 1;
                        WriteLine("You take out your axe and hysterically swing it knocking out 1 cannibal\ncausing the other cannibals to step back for a moment, but eventually decided to continue to charge.");
                        Thread.Sleep(4000);
                    }
                }
                WriteLine($"With nothing else in your arsenal, you run towards the water.\nUnfortunately, the remaining {cannibals} cannibals devour you.");
                Thread.Sleep(4000);
                endGame();
            }
            else if (cannibals == 1)
            {
                if (inventory.Contains("Gun"))
                {
                    WriteLine("With 1 cannibal remaining, you shoot him as he charges at you.");
                    Thread.Sleep(4000);
                    WriteLine("The onslaught is over. The rescue boat arrives.");
                    Thread.Sleep(4000);
                    WriteLine("You made it home.");
                    Thread.Sleep(3000);
                }
                else if (!inventory.Contains("Gun"))
                {
                    if (inventory.Contains("Axe"))
                    {
                        WriteLine("With 1 cannibal remaining, he charges at you.");
                        Thread.Sleep(3000);
                        WriteLine("You fight him back with your axe and manage to hit him.");
                        Thread.Sleep(3000);
                        WriteLine("The wound is deep. But not enough to kill him immediately.");
                        Thread.Sleep(3000);
                        WriteLine("He takes you down and eats you alive. As your eyes slowly close,\nthe cannibal slowly stops eating as he succumbs to his wounds.\nUnfortunately, you succumb to his appetite.");
                        Thread.Sleep(4000);
                    }
                }
            }
            endGame();
        }
        // End Game Method
        private void endGame()
        {
            WriteLine("======================================================");
            WriteLine("Thanks for playing Island!");
            WriteLine("======================================================");
            System.Environment.Exit(0);
        }
    }
}