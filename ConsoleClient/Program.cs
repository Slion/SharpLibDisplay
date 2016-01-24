using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ServiceModel;
using SharpLib.Display;

namespace ConsoleClient
{
    class Program
    {

        static Client iClient;

        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("SharpLibDisplay console client.");
            Console.WriteLine("Commands:");
            Console.WriteLine("   q: quit");
            Console.WriteLine("   c: close client");
            Console.WriteLine("   o: open client");
            Console.WriteLine("   l: set layout");
            Console.WriteLine("   stf: set text field");
            Console.WriteLine("   sbf: set bitmap field");
            Console.WriteLine("   srf: set recording field");
            Console.WriteLine("-----------------------------------");

            //Create our client and connect to our server
            iClient = new Client();
            iClient.CloseOrderEvent += OnCloseOrder;

            OpenClient();

            CommunicationState state = iClient.SessionState;

            Console.WriteLine("State: " + state.ToString());

            bool quit = false;
            while (!quit)
            {
                string command = Console.ReadLine();
                quit = DispatchCommand(command);

                //Output our state if it was changed
                if (state != iClient.SessionState)
                {
                    //State has changed
                    state = iClient.SessionState;
                    Console.WriteLine("State: " + state.ToString());
                }
            }

            iClient.Close();

        }


        public static bool DispatchCommand(string aCommand)
        {
            switch (aCommand)
            {
                case "q":
                    return true;

                case "c":
                    iClient.Close();
                    break;

                case "l":
                    SetLayout();
                    break;

                case "o":
                    OpenClient();
                    break;

                case "stf":
                    SetTextField();
                    break;

                case "sbf":
                    SetBitmapField();
                    break;

                case "srf":
                    SetRecordingField();
                    break;

            }

            return false;
        }

        static void OpenClient()
        {
            iClient.Open("net.tcp://localhost:8111/DisplayService");

            //Connect using unique name
            string name = "Client-" + (iClient.ClientCount() - 1);
            iClient.SetName(name);
            iClient.SetPriority(Priorities.Default);
            Console.WriteLine("Name: " + name);
        }

        public static void SetLayout()
        {
            TableLayout layout = new TableLayout(1, 1);
            iClient.SetLayout(layout);
            //We need to create our fields
            DataField field = new TextField();
            DataField recordingField = new RecordingField();

            iClient.CreateFields(new DataField[]
            {
                field,
                recordingField
            });
        }

        public static void SetTextField()
        {
            DataField field = new TextField();
            if (!iClient.SetField(field))
            {
                Console.WriteLine("ERROR: field not found! Check layout and field creation.");
            }
        }

        public static void SetBitmapField()
        {
            DataField field = new BitmapField();
            if (!iClient.SetField(field))
            {
                Console.WriteLine("ERROR: field not found! Check layout and field creation.");
            }
        }

        public static void SetRecordingField()
        {
            DataField field = new RecordingField();
            if (!iClient.SetField(field))
            {
                Console.WriteLine("ERROR: field not found! Check layout and field creation.");
            }
        }

        public static void OnCloseOrder()
        {
            Console.WriteLine("Close order!");
            iClient.Close();
            //CloseThreadSafe();
        }

    }
}
