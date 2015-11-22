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
            Console.WriteLine("   q: close client");
            Console.WriteLine("-----------------------------------");

            //Create our client and connect to our server
            iClient = new Client();
            iClient.CloseOrderEvent += OnCloseOrder;
            iClient.Open("net.tcp://localhost:8111/DisplayService");

            //Connect using unique name
            //string name = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            string name = "Client-" + (iClient.ClientCount() - 1);
            iClient.SetName(name);
            //Text = Text + ": " + name;
            //Text = "[[" + name + "]]  " + iClient.SessionId;

            Console.WriteLine("Name: " + name);


            CommunicationState state = iClient.SessionState;

            Console.WriteLine("State: " + state.ToString());

            while (Console.ReadLine() != "q")
            {
                if (state != iClient.SessionState)
                {
                    //State has changed
                    state = iClient.SessionState;
                    Console.WriteLine("State: " + state.ToString());
                }

            }

            iClient.Close();

        }

        public static void OnCloseOrder()
        {
            Console.WriteLine("Close order!");
            iClient.Close();
            //CloseThreadSafe();
        }

    }
}
