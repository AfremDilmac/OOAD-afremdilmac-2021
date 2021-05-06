using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfEscapeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Room currentRoom;
        Room computerRoom;
        Room livingRoom;
        Door greenDoor;
        public MainWindow()
        {
            InitializeComponent();
            // define room
            Room room1 = new Room("bedroom", "I seem to be in a medium sized bedroom. There is a locker to the left with a chair next to it, a nice rug on the floor and a bed to the right with a poster above the bed. ");
            Room room2 = new Room("computer", "I seem to be in a little sized computer room. There is a computer to the left on a desktop with a chair next to it and a frame at the wall. There is a flag on the wall a radiator and 2 chairs one at the left side of the door and another one at the right side. ");
            Room room3 = new Room("living", "I seem to be in a big sized living room. I can see a chair, a clock, a plant a floor mat and a door with a pincode to open the door ?");
            Door groenendeur = new Door("Green door", "A locked green door, i have to find a way to open this");
            // define items
            //Doors
         
            //Living room items
            Item chair3 = new Item("Chair", "It's a random chair, nothing special.");
            Item clock = new Item("Clock", "A Clock, can this frame actually help me ? Maybe can i use the time.");
            Item plant = new Item("Plant", "A Plant, Maybe there is something hide in this plant, i mean we never know?");
            Item pincode = new Item("pincode", "A pincode, i wonder what is the code.");
            //Computer room items
            Item computer = new Item("computer", "A computer, can i use it ?");
            Item frame = new Item("frame", "A Frame, someone could hide something on this frame?");
            Item chair2 = new Item("Chair", "It's a random chair, nothing special.");
            Item flag = new Item("flag", "A Flag, can this frame actually help me ?");
            Item radiator = new Item("radiator", "Can this thing even heat me ?");
            Item bin = new Item("bin", "There is probably something hide in this ?");
            //Bedroom items
            Item key1 = new Item("small silver key", "A small silver key, makes me think of one I had at highschool. ");
            Item key2 = new Item("large key", "A large key. Could this be my way out? ");
            Item locker = new Item("locker", "A locker. I wonder what's inside. ");
            Item chair = new Item("Chair", "It's a random chair, nothing special.");
            Item poster = new Item("Poster", "A poster. Can i find something on it to help me?");
            Door grendoor = new Door("door", "test");
            locker.HiddenItem = key2;
            locker.IsLocked = true;
            locker.Key = key1;
            Item bed = new Item("bed", "Just a bed. I am not tired right now. ");
            bed.IsPortable = false;
            chair.IsPortable = false;
            locker.IsPortable = false;
            bed.HiddenItem = key1;
            
            // setup bedroom
            room1.Items.Add(new Item("floor mat", "A bit ragged floor mat, but still one of the most popular designs. "));
            room1.Items.Add(bed);
            room1.Items.Add(locker);
            room1.Items.Add(chair);
            room1.Items.Add(poster);
           
            //setup living room
            room3.Items.Add(new Item("floor mat", "A bit ragged floor mat, but still one of the most popular designs. "));
            room3.Items.Add(chair3);
            room3.Items.Add(clock);
            room3.Items.Add(plant);
            room3.Items.Add(pincode);
            //setup computer room
            room2.Items.Add(computer);
            room2.Items.Add(frame);
            room2.Items.Add(chair2);
            room2.Items.Add(flag);
            room2.Items.Add(computer);
            room2.Items.Add(bin);
            room2.Items.Add(radiator);
            //setup doors
            groenendeur.IsLocked = true;


            // start game
            greenDoor = groenendeur;
            currentRoom = room1;
            computerRoom = room2;
            livingRoom = room3;
            lblMessage.Content = "I am awake, but cannot remember who I am!? Must have been a hell of a party last night... ";
            txtRoomDesc.Text = currentRoom.Description;
            UpdateUI();
        }
        private void UpdateUI()
        {
            lstRoomItems.Items.Clear();
            foreach (Item itm in currentRoom.Items)
            {
                lstRoomItems.Items.Add(itm);
            }
            lstDoors.Items.Clear();
            foreach (Item door in greenDoor.Items)
            {
                lstDoors.Items.Add(door);
            }
        }
        private void LstItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCheck.IsEnabled = lstRoomItems.SelectedValue != null; // room item selected
            btnPickUp.IsEnabled = lstRoomItems.SelectedValue != null; // room item selected
            btnUseOn.IsEnabled = lstRoomItems.SelectedValue != null && lstMyItems.SelectedValue != null;
            btnDrop.IsEnabled = lstMyItems.SelectedValue != null;
            // room item
            //and picked up item selected
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            // 1. find item to check
            Item roomItem = (Item)lstRoomItems.SelectedItem;
            // 2. is it locked?
            if (roomItem.IsLocked)
            {
                lblMessage.Content = $"{roomItem.Description}It is firmly locked. ";
                return;
            }
            // 3. does it contain a hidden item?
            Item foundItem = roomItem.HiddenItem;
            if (foundItem != null)
            {
                lblMessage.Content = $"Oh, look, I found a {foundItem.Name}";
                lstMyItems.Items.Add(foundItem);
                roomItem.HiddenItem = null;
                return;
            }
            // 4. just another item; show description
            lblMessage.Content = roomItem.Description;
        }
     
        private void BtnUseOn_Click(object sender, RoutedEventArgs e)
        {

            // 1. find both items
            Item myItem = (Item)lstMyItems.SelectedItem;
            Item roomItem = (Item)lstRoomItems.SelectedItem;
            // 2. item doesn't fit
            Random rnd = new Random();
            int a = rnd.Next(0, 3);
 
            if (roomItem.Key != myItem)
            {
                switch (a)
                {
                    case 1:
                        lblMessage.Content = RandomMessageGenerator.GetRandomMessage(RandomMessageGenerator.MessageType.Message0);
                        break;
                    case 2:
                        lblMessage.Content = RandomMessageGenerator.GetRandomMessage(RandomMessageGenerator.MessageType.Message1);
                        break;
                    default:
                        lblMessage.Content = RandomMessageGenerator.GetRandomMessage(RandomMessageGenerator.MessageType.Message2);
                        break;
                }
                return;
            }
            // 3. item fits; other item unlocked
            roomItem.IsLocked = false;
            roomItem.Key = null;
            lstMyItems.Items.Remove(myItem);
            lblMessage.Content = $"I just unlocked the {roomItem.Name}!";
        }

        private void BtnPickUp_Click(object sender, RoutedEventArgs e)
        {
            // 1. find selected item
            Item selItem = (Item)lstRoomItems.SelectedItem;

            if (selItem.IsPortable == false)
            {
                lblMessage.Content = $"I can't take the {selItem.Name}. ";
            }
            else if (selItem.IsPortable == true)
            {
                lblMessage.Content = $"I just picked up the {selItem.Name}. ";
                lstMyItems.Items.Add(selItem);
                lstRoomItems.Items.Remove(selItem);
                currentRoom.Items.Remove(selItem);
            }
            // 2. add item to your items list

        }
        private void BtnDrop_Click(object sender, RoutedEventArgs e)
        {
            // 1. find selected item
            Item myItem = (Item)lstMyItems.SelectedItem;
            // 2. add item to your items list
            lblMessage.Content = $"I just dropped the {myItem.Name}. ";
            lstMyItems.Items.Remove(myItem);
            lstRoomItems.Items.Add(myItem);
            currentRoom.Items.Add(myItem);
        }

        private void LstDoors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
