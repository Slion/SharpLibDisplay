using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLib.Display;
using System.ServiceModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace ConsoleServer
{
    /// <summary>
    /// Implement our display services.
    /// Each client connection has such a session object server side.
    /// </summary>
    [ServiceBehavior(
                        ConcurrencyMode = ConcurrencyMode.Multiple,
                        InstanceContextMode = InstanceContextMode.PerSession
                    )]
    public class ServerSession : IService, IDisposable
    {
        public string SessionId { get; set; }
        public string Name { get; set; }
        public ICallback Callback { get; set; }

        ServerSession()
        {
            
            //First save our session ID. It will be needed in Dispose cause our OperationContxt won't be available then.
            SessionId = OperationContext.Current.SessionId;
            Callback = OperationContext.Current.GetCallbackChannel<ICallback>();
            ConsoleTrace();
            ConsoleServer.Program.AddClient(this);            
        }

        ~ServerSession()
        {
            ConsoleTrace();
        }

        /// <summary>
        /// Called when client is closed properly.
        /// </summary>
        public void Dispose()
        {
            ConsoleTrace();

            ConsoleServer.Program.RemoveClient(this);
        }

        //
        public void SetName(string aClientName)
        {
            ConsoleTrace();
            Name = aClientName;
            //SharpDisplayManager.Program.iMainForm.SetClientNameThreadSafe(SessionId, Name);
            //Disconnect(aClientName);

            //Register our client and its callback interface
            //IDisplayServiceCallback callback = OperationContext.Current.GetCallbackChannel<IDisplayServiceCallback>();
            //Program.iMainForm.iClients.Add(aClientName, callback);
            //Program.iMainForm.treeViewClients.Nodes.Add(aClientName, aClientName);
            //For some reason MP still hangs on that one
            //callback.OnConnected();
        }

        public void SetLayout(TableLayout aLayout)
        {
            ConsoleTrace();
            //SharpDisplayManager.Program.iMainForm.SetClientLayoutThreadSafe(SessionId, aLayout);
        }

        //
        public void SetField(DataField aField)
        {
            ConsoleTrace(" - " + aField.GetType().Name);            
            

            //SharpDisplayManager.Program.iMainForm.SetClientFieldThreadSafe(SessionId, aField);
        }

        //From IDisplayService
        public void SetFields(System.Collections.Generic.IList<DataField> aFields)
        {
            ConsoleTrace();
            //SharpDisplayManager.Program.iMainForm.SetClientFieldsThreadSafe(SessionId, aFields);
        }

        ///
        public int ClientCount()
        {
            ConsoleTrace();
            return ConsoleServer.Program.iClients.Count;
            //return SharpDisplayManager.Program.iMainForm.iClients.Count;
        }

        /// <summary>
        /// Define standard console output upon receiving client requests.
        /// </summary>
        void ConsoleTrace(string aExtra="")
        {
            //Using reflection framework to get class and method name.
            Console.WriteLine(SessionId + ": " + MethodAt(2).ReflectedType.Name + "::" + MethodAt(2).Name + aExtra);
        }

        /// <summary>
        /// Convenient way to get a method from our call stack.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        MethodBase MethodAt(int aIndex)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(aIndex);

            return sf.GetMethod();
        }

    }

}