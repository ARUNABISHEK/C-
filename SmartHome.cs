using System;
using SmartHome;
using SmartHome.Views;
using SmartHome.Models;
using SmartHome.Interfaces;
using SmartHome.Classes;

namespace SmartHome {

    namespace Interfaces {

        interface Electronics {
            int GadgetNo {get; set;}
            string Name {get;set;}
            bool switchStatus{get; set;}
        }
        
        interface TwoWayConnection {
            Electronics Connection {get; set;}
        }

        interface Room {
            Switch[] getSwitches { get; }
            string RoomName{get; set;}
        }

    }


    namespace Models {

        class SwitchModel {
            
            public int switchNo {get; set;}
            public bool isTwoWaySwitch {get; set;}  
            public bool switchStatus {get; set;}
            public Electronics connection = null;
        }

        enum Rooms {
            MasterBedRoom = 2,
            SecondaryBedRoom,
            Kitchen
        }

    }

    namespace Classes {

        class Home {
            
            private Room[] room;
            
            public Home(params Room[] rooms) {
                room = new Room[rooms.Length];
                int i = 0;
                foreach (Room room in rooms) {
                    this.room[i++] = room;
                }
            }
            
            public Room GetBedRoom(int index) {
                return room[index];
            }
            // public MasterBedRoom GetMasterBedRoom {
            //     get {
            //         return (MasterBedRoom)this.room[0];
            //     }
            // }

            // public SecondaryBedRoom GetSecondaryBedRoom {
            //     get {
            //         return (SecondaryBedRoom)this.room[1];
            //     }
            // }

            // public Kitchen GetKitchen {
            //     get {
            //         return (Kitchen)this.room[2];
            //     }
            // }
        }

        //Home inside electronic items
        class RoomArangements {

            public void setFanAndLights(int NumberOfSwitches, Switch[] switches) {
 
                    int lightNo = 1;
                    int fanNo = 1;
                    
                    for(int i=1;i<=NumberOfSwitches;i++) {

                        Electronics gadget = null;
                        if(i==3) {
                            gadget =(Electronics) new TwoWay(switches[0].connection,3,"Two way switch for Switch 1");
                            switches[i-1] = new Switch(i,true,gadget);
                        }

                        else {
                            if(i==1 || i==4) {
                                gadget =(Electronics) new Light(lightNo++);
                            }
                            else {
                                gadget =(Electronics) new Fan(fanNo++);
                            }
                            switches[i-1] =  new Switch(i,gadget);
                        }
                        
                    }
            }
        }
        
        class Light : Electronics {
            private int lightNo;
            private string name;
            private bool status = false;
            public Light(int lightNo) {
                this.lightNo = lightNo;
                this.name = "Light" + this.lightNo.ToString();
            }

            public int GadgetNo {
                get {
                    return lightNo;
                }
                set {
                    lightNo = value;
                }
            }
            
            public string Name {
                get{
                    return name;
                }
                set {
                    name = value;
                }
            }
           public bool switchStatus {
               get{
                   return status;
               }
               set {
                   status = value;
               }
           }
        }

        class Fan : Electronics {
            private int fanNo;
            private string name;
            private bool status = false;
            public Fan(int fanNo) {
                this.fanNo = fanNo;
                this.name = "Fan" + this.fanNo.ToString();
                
            }

            public int GadgetNo {
                get {
                    return fanNo;
                }
                set {
                    fanNo = value;
                }
            }
            
            public string Name {
                get{
                    return name;
                }
                set{
                    name = value;
                }
            }
            public bool switchStatus {
               get{
                   return status;
               }
               set {
                   status = value;
               }
           }
        }

        class MasterBedRoom : Room {

            private Switch[] switches;
            private string name = "MasterBedRoom";
            public MasterBedRoom(int NumberOfSwitches) {
                
                switches = new Switch[NumberOfSwitches];
                new RoomArangements().setFanAndLights(NumberOfSwitches, switches);
            }

            public Switch[] getSwitches {
                get {
                    return this.switches;
                }
            }
            
            public string RoomName {
                get {
                    return name;
                }
                set {
                    name = value;
                }
            }

        }

        }

        class SecondaryBedRoom : Room {

            private Switch[] switches;
            private string name = "MasterBedRoom";
            
            public SecondaryBedRoom(int NumberOfSwitches) {
                
                switches = new Switch[NumberOfSwitches];
                new RoomArangements().setFanAndLights(NumberOfSwitches, switches);
            }

            public Switch[] getSwitches {
                get {
                    return this.switches;
                }
            }
            
            public string RoomName {
                get {
                    return name;
                }
                set {
                    name = value;
                }
            }
        }

        class Kitchen : Room {

            private Switch[] switches;
            private string name = "MasterBedRoom";
            
            public Kitchen(int NumberOfSwitches) {
                
                switches = new Switch[NumberOfSwitches];
                new RoomArangements().setFanAndLights(NumberOfSwitches, switches);

            }

            public Switch[] getSwitches {
                get {
                    return this.switches;
                }
            }
            
            public string RoomName {
                get {
                    return name;
                }
                set {
                    name = value;
                }
            }
        }

        class Switch : SwitchModel {
            
            public Switch(int switchNo, Electronics connection) {
                
                base.switchNo = switchNo;
                base.connection = connection;
            }

            public Switch(int switchNo, bool isTwoWaySwitch, Electronics connection) {
                base.switchNo = switchNo;
                base.isTwoWaySwitch = isTwoWaySwitch;
                
                base.connection = connection;
            }
            
            public Electronics GetConnection{
                get {
                    return base.connection;
                }
            }
        }

        class TwoWay : Electronics,TwoWayConnection {
            private int switchNo;
            private string name;
            private Electronics con;
            private bool status = false;
            
            public TwoWay(Electronics connection,int lightNo, string name) {
                this.con = connection;
                this.switchNo = lightNo;
                this.name = name;
            }

            public int GadgetNo {
                get {
                    return switchNo;
                }
                set {
                    switchNo = value;
                }
            }

            public string Name {
                get{
                    return name;
                }
                set {
                    name = value;
                }
            }

            public bool switchStatus {
                get{
                    
                    return status;
                }
                set {
                    status = value;
                }
            }

            public Electronics Connection {
                get {
                    return this.con;
                }
                set {
                    this.con = value;
                }
            } 
        }

    namespace Views {

        struct GetLine {
            public static void Draw() {
                Console.WriteLine("-----------------------------");
            }
            public static void Underline() {
                Console.WriteLine("--------------");
            }
        }

        struct WelcomeMessage {
            public static void Message() {
                Console.WriteLine("Welcome to Smart Home");
                GetLine.Draw();
            }
            
        }
        
        struct RoomMenu {

            public static void AvailableRooms() {
                Console.WriteLine(((int)Rooms.MasterBedRoom).ToString()+"."+Rooms.MasterBedRoom.ToString());
                Console.WriteLine(((int)Rooms.SecondaryBedRoom).ToString()+"."+Rooms.SecondaryBedRoom.ToString());
                Console.WriteLine(((int)Rooms.Kitchen).ToString()+"."+Rooms.Kitchen.ToString());
            }
        }
        
        struct OptionMenu {
            
            public static void Options() {
                Console.WriteLine("1.View status in all Rooms");
                RoomMenu.AvailableRooms();
                Console.WriteLine("0.Exit");
                GetLine.Draw();
            }
        }
        
        class ShowAllRooms {
            
            public static void Show(Home home) {
                
                for(int i=0;i<Enum.GetNames(typeof(Rooms)).Length;i++) {
                            
                    Rooms room = (Rooms)(i + (int)Rooms.MasterBedRoom);
                    Switch[] switches;
                    
                    if(room.ToString() == "MasterBedRoom") {
                        Console.WriteLine(room.ToString());
                        GetLine.Underline();
                        //switches = home.GetMasterBedRoom.getSwitches;
                    switches = home.GetBedRoom((int)Rooms.MasterBedRoom-2).getSwitches;
                    } else if(room.ToString() == "SecondaryBedRoom") {
                        Console.WriteLine(room.ToString());
                        GetLine.Underline();
                        //switches = home.GetSecondaryBedRoom.getSwitches;
                        switches = home.GetBedRoom((int)Rooms.SecondaryBedRoom-2).getSwitches;
                    } else {
                        Console.WriteLine(room.ToString());
                        GetLine.Underline();
                        //switches = home.GetKitchen.getSwitches;
                        switches = home.GetBedRoom((int)Rooms.Kitchen-2).getSwitches;
                        
                    }
                
                    foreach(Switch @switch in switches) {
                        if(@switch.isTwoWaySwitch) {
                            continue;
                        }
                        if(@switch.switchStatus) {
                            Console.WriteLine(@switch.connection.Name + " :- ON");
                        } else {
                            Console.WriteLine(@switch.connection.Name + " :- OFF");
                        }
                    }
                    GetLine.Draw();
                }
            }
        }
        
        struct Input {
            public static int getIntInput() {
                Console.Write("Enter your option : ");
                int choice = -1;
                try {
                    choice = Convert.ToInt32(Console.ReadLine());
                }

                catch (System.Exception){}

                return choice;
            } 
        }
        
        class ShowRoomData {
            
            public static void Show(Room room) {
                
                Switch[] switches = room.getSwitches;
                
                Console.WriteLine(room.RoomName);
            doPerform:
                int i=1;
                foreach(Switch @switch in switches) {
                    Console.WriteLine(i++ + ".Switch "+@switch.switchNo +"(" + @switch.connection.Name + ")");
                }
                
                Console.Write("Enter the switch :- ");
                int EnteredSwitchNumber = 0;
                try {
                    int flag = Convert.ToInt32(Console.ReadLine());
                    if(flag <= switches.Length && flag > 0)
                        EnteredSwitchNumber = flag;
                    else {
                        Console.WriteLine("Wrong switch number");
                        return;
                    }
                } catch(Exception) {
                    Console.WriteLine("Wrong input");
                    return;
                }
                
                if(switches[EnteredSwitchNumber-1].isTwoWaySwitch) {
                    TwoWay sw = (TwoWay) switches[EnteredSwitchNumber-1].GetConnection;
                    Electronics connection = sw.Connection;
                    
                    
                    if(switches[0].switchStatus) {
                        
                        foreach(Switch switch1 in switches) {
                            if(switch1.GetConnection == connection) {
                                switch1.switchStatus = false;
                            }
                        }
                    } else {
                        
                        Console.Write("Turn on (Y or N) :- ");
                        char isSwitchOn;
                        try {
                            isSwitchOn = Convert.ToChar(Console.ReadLine());
                        } catch(System.Exception) {
                            Console.WriteLine("Wrong input");
                            return;
                        }
                        
                        if(isSwitchOn == 'y' || isSwitchOn == 'Y') {
                            
                            foreach(Switch switch1 in switches) {
                                if(switch1.GetConnection == connection) {
                                        switch1.switchStatus = true;
                                }
                            }
                        } 
                        
                    }
                    
                } else {
                    if(switches[EnteredSwitchNumber-1].switchStatus) {
                        switches[EnteredSwitchNumber-1].switchStatus = false;
                    } else {
                        Console.Write("Turn on (Y or N) :- ");
                        char isSwitchOn;
                        try {
                            isSwitchOn = Convert.ToChar(Console.ReadLine());
                        } catch(System.Exception) {
                            Console.WriteLine("Wrong input");
                            return;
                        }
                        
                        if(isSwitchOn == 'y' || isSwitchOn == 'Y') {
                            switches[EnteredSwitchNumber-1].switchStatus = true;
                        } 
                    }
                }
                
                foreach(Switch @switch in switches) {
                    if(@switch.isTwoWaySwitch) {
                        continue;
                    }
                    if(@switch.switchStatus) {
                        Console.WriteLine(@switch.connection.Name + " :- ON");
                    } else {
                        Console.WriteLine(@switch.connection.Name + " :- OFF");
                    }
                }
                            
                while(true) {
                    Console.Write("Want to turn on/off switches in same room(Y or N) :- ");
                    char isContinue;
                    try {
                        isContinue = Convert.ToChar(Console.ReadLine());
                    } catch(Exception) {
                        Console.WriteLine("Wrong input");
                        return;
                    }
                    
                    
                    if(isContinue == 'y' || isContinue == 'Y') {
                        goto doPerform;
                    }else if(isContinue=='N' || isContinue == 'n'){
                        Console.Write("Want to switch another room (Y/N) :- ");
                        try {
                            isContinue = Convert.ToChar(Console.ReadLine());
                        } catch(Exception) {
                            Console.WriteLine("Wrong input");
                            return;
                        }
                        if(isContinue=='y' || isContinue=='Y') {
                            break;
                        } else {
                            Console.WriteLine("ThankYou!");
                            System.Environment.Exit(0);
                        }
                        
                    } else {
                        Console.WriteLine("Wrong Selection");
                    }     
                    
                
                }
                        
                GetLine.Draw();
            }
        }
    }

    public class MainHomePage {
        static void Main(String[] args) {
            
            WelcomeMessage.Message();
           
            
            Room masterBedRoom = new MasterBedRoom(5);
            Room secondaryBedRoom = new SecondaryBedRoom(5);
            Room kitchen = new Kitchen(5);

            Home home = new Home(masterBedRoom,secondaryBedRoom,kitchen);
            Room room = null;   //Master,Secondary,Kitchan
            
            int choice = -1;
            
            do {
                
                OptionMenu.Options();
            
                choice = Input.getIntInput();
                GetLine.Draw();
                
                
                switch(choice) {
                
                    case 0:
                        Console.WriteLine("Thankyou");
                        System.Environment.Exit(0);
                        break;
                      
                    case 1:
                        //Show all room status
                        
                        ShowAllRooms.Show(home);
                        break;
                        
                    case (int)Rooms.MasterBedRoom :
                        room = home.GetBedRoom((int)Rooms.MasterBedRoom-2);
                        //room = home.GetMasterBedRoom;
                        ShowRoomData.Show(room);
                        break;
                     
                    case (int)Rooms.SecondaryBedRoom :
                        room = home.GetBedRoom((int)Rooms.SecondaryBedRoom-2);
                        // room = home.GetSecondaryBedRoom;
                        ShowRoomData.Show(room);
                        break;
                        
                    case (int)Rooms.Kitchen :
                        room = home.GetBedRoom((int)Rooms.Kitchen-2);
                        // room = home.GetKitchen;
                        ShowRoomData.Show(room);
                        break;
                    default:
                        Console.WriteLine("Wrong input"); 
                        break;
                }
            }while(choice!=0); 
        }
    }
}