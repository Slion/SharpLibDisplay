using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels; // For Binding
using SharpLib.Display;

namespace ConsoleServer
{
    /// <summary>
    /// Implement a console server for testing our client APIs.
    /// Can also be used as base for production implementations.
    /// </summary>
    public class Program
    {
        static ServiceHost iServiceHost;
        public static List<ServerSession> iClients;

        //[STAThread]
        static public void MainThreadStart()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("SharpLibDisplay console server.");
            Console.WriteLine("Commands:");
            Console.WriteLine("   q: close server");
            Console.WriteLine("-----------------------------------");


            StartServer("net.tcp://localhost:8111/");

            CommunicationState state = iServiceHost.State;

            Console.WriteLine("State: " + state.ToString());

            while (Console.ReadLine() != "q")
            {
                if (state != iServiceHost.State)
                {
                    //State has changed
                    state = iServiceHost.State;
                    Console.WriteLine("State: " + state.ToString());
                }

            }

            StopServer();

        }

        static public void Main(string[] args)
        {
            MainThreadStart();
        }

        static public void StartServer(string aBaseUri = "net.tcp://localhost:8001/", string aAddress = "DisplayService")
        {
            iClients = new List<ServerSession>();
            iServiceHost = new ServiceHost
                (
                    typeof(ServerSession),
                    new Uri[] { new Uri(aBaseUri) }
                );

            Binding binding = new NetTcpBinding(SecurityMode.None, true);
            // Here we define our timeouts
            // See: https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/configuring-timeout-values-on-a-binding
            // I believe they should all be set to 10mn by default, let's just set them to 1 mn for testing purposes
            binding.OpenTimeout = new TimeSpan(0, 1, 0);
            binding.CloseTimeout = new TimeSpan(0, 1, 0);
            binding.SendTimeout = new TimeSpan(0, 1, 0);
            // Lower this so that we can test and debug our falted issue
            // Make it higher if you don't want timeout after 10 seconds
            binding.ReceiveTimeout = new TimeSpan(0, 0, 10);

            iServiceHost.AddServiceEndpoint(typeof(IService), binding, aAddress);
            iServiceHost.Open();
        }

        static public void StopServer()
        {
            //First tell our clients we are shuting down
            BroadcastCloseEvent();
            //Then shutdown
            iServiceHost.Close();
        }

        static public void BroadcastCloseEvent()
        {
            Console.WriteLine("BroadcastCloseEvent - start");

            var inactiveClients = new List<string>();
            foreach (var client in iClients)
            {
                //if (client.Key != eventData.ClientName)
                {
                    try
                    {
                        Console.WriteLine("BroadcastCloseEvent - " + client.SessionId);
                        client.Callback.OnCloseOrder(/*eventData*/);
                    }
                    catch //(Exception ex)
                    {
                        //inactiveClients.Add(client.Key);
                    }
                }
            }

        }


        static public void AddClient(ServerSession aClient)
        {
            iClients.Add(aClient);
            Console.WriteLine("Client count: " + iClients.Count);
        }

        static public void RemoveClient(ServerSession aClient)
        {
            iClients.Remove(aClient);
           Console.WriteLine("Client count: " + iClients.Count);
        }



    }
}
