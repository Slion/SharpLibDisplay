﻿// Copyright (C) 2014-2016 Stéphane Lenclud.
//
// This file is part of SharpLibDisplay.
//
// SharpLibDisplay is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// SharpLibDisplay is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with SharpLibDisplay.  If not, see <http://www.gnu.org/licenses/>.
//


using System;
using System.ServiceModel;


namespace SharpLib.Display
{

    /// <summary>
    /// Handle connection with our Sharp Display Server.
    /// If the connection is faulted it will attempt to restart it.
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Client: ICallback, IDisposable
    {
        private ClientSession iClient;
        private bool resetingConnection = false;
        string iAddress = "";

        /// <summary>
        /// 
        /// </summary>
        public CommunicationState SessionState
        {
            get
            {   
                if (iClient!=null)
                {
                    return iClient.State;
                }

                return CommunicationState.Closed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string SessionId
        {
            get
            {
                if (iClient != null)
                {
                    return iClient.SessionId;
                }

                return "";                
            }
        }
        public string Name { get; private set; }
        public uint Priority { get; private set; }
        public Target Target { get; private set; }
        private TableLayout Layout { get; set; }
        private TableLayout DisplayLayout { get; set; }
        private System.Collections.Generic.IList<DataField> Fields { get; set; }

        public delegate void CloseOrderDelegate();
        public event CloseOrderDelegate CloseOrderEvent;


        public Client()
        {
            Fields = new DataField[]{};
            Close();
        }

        /// <summary>
        /// Initialize our server connection.
        /// </summary>
        public void Open(string aEndpointAddress = "net.tcp://localhost:8001/DisplayService")
        {
            iAddress = aEndpointAddress;
            Close(); //Make sure we close any earlier connection
            iClient = new ClientSession(this, aEndpointAddress);            
        }

        /// <summary>
        /// Terminate our server connection.
        /// </summary>
        public void Close()
        {
            if (IsReady())
            {
                iClient.Close();
            }
            iClient = null;
            Target = Target.Client;
            Name = "";
        }

        /// <summary>
        /// Tells whether a server connection is available.
        /// </summary>
        /// <returns>True if a server connection is available. False otherwise.</returns>
        public bool IsReady()
        {
            return (iClient != null && iClient.IsReady());
        }

        /// <summary>
        /// Check if our server connection is available and attempt reset it if it isn't.
        /// This is notably dealing with timed out connections.
        /// </summary>
        public void CheckConnection()
        {
            if (!IsReady() && !resetingConnection)
            {
                //Try to reconnect
                string name = Name; // preserve our name
                Open(iAddress); // Make sure we use the proper address

                //Avoid stack overflow in case of persisting failure
                // TODO: use mutex?
                resetingConnection = true;

                try
                {
                    //On reconnect there is a bunch of properties we need to reset
                    if (name != "")
                    {
                        Name = name;
                        iClient.SetName(Name);
                    }

                    SetLayout(Layout);
                    iClient.SetFields(Fields);
                }
                finally
                {
                    //Make sure our state does not get out of sync
                    resetingConnection = false;
                }
            }
        }

        /// <summary>
        /// Set our client's name.
        /// Client's name is typically user friendly.
        /// It does not have to be unique.
        /// </summary>
        /// <param name="aClientName">Our client name.</param>
        public void SetName(string aClientName)
        {
            Name = aClientName;
            CheckConnection();
            iClient.SetName(aClientName);
        }

        /// <summary>
        /// Set client priority.
        /// </summary>
        /// <param name="aPriority"></param>
        public void SetPriority(uint aPriority)
        {
            Priority = aPriority;
            CheckConnection();
            iClient.SetPriority(aPriority);
        }

        /// <summary>
        /// Set client target.
        /// </summary>
        /// <param name="aPriority"></param>
        public void SetTarget(Target aTarget)
        {
            Target = aTarget;
            CheckConnection();
            iClient.SetTarget(aTarget);
        }

        /// <summary>
        /// Set your client fields' layout.
        /// </summary>
        /// <param name="aLayout">The layout to apply for this client.</param>
        public void SetLayout(TableLayout aLayout)
        {
            Layout = aLayout;
            CheckConnection();
            iClient.SetLayout(aLayout);
        }

        /// <summary>
        /// Set the specified field.
        /// </summary>
        /// <param name="aField"></param>
        /// <returns>True if the specified field was set client side. False means you need to redefine all your fields using CreateFields.</returns>
        public bool SetField(DataField aField)
        {
            int i = 0;
            bool fieldFound = false;
            foreach (DataField field in Fields)
            {
                if (field.IsSameAs(aField))
                {
                    //Update our field then
                    Fields[i] = aField;
                    fieldFound = true;
                    break;
                }
                i++;
            }

            if (!fieldFound)
            {
                //Field not found, make sure to use CreateFields first after setting your layout.
                return false;
            }

            CheckConnection();
            iClient.SetField(aField);
            return true;
        }

        /// <summary>
        /// Use this function when updating existing fields.
        /// </summary>
        /// <param name="aFields"></param>
        public bool SetFields(System.Collections.Generic.IList<DataField> aFields)
        {
            int fieldFoundCount = 0;
            foreach (DataField fieldUpdate in aFields)
            {
                int i = 0;
                foreach (DataField existingField in Fields)
                {
                    if (existingField.IsSameAs(fieldUpdate))
                    {
                        //Update our field then
                        Fields[i] = fieldUpdate;
                        fieldFoundCount++;
                        //Move on to the next field
                        break;
                    }
                    i++;
                }
            }

            //
            if (fieldFoundCount!=aFields.Count)
            {
                //Field not found, make sure to use CreateFields first after setting your layout.
                return false;
            }

            CheckConnection();
            iClient.SetFields(aFields);
            return true;
        }

        /// <summary>
        /// Use this function when creating your fields.
        /// This must be done at least once after setting your layout.
        /// Fields must be specified by column.
        /// </summary>
        /// <param name="aFields"></param>
        public void CreateFields(System.Collections.Generic.IList<DataField> aFields)
        {
            Fields = aFields;
            CheckConnection();
            iClient.SetFields(aFields);
        }

        /// <summary>
        /// Provide the number of clients currently connected to our server.
        /// </summary>
        /// <returns>Number of clients currently connected to our server.</returns>
        public int ClientCount()
        {
            CheckConnection();
            return iClient.ClientCount();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aEventName"></param>
        /// <returns></returns>
        public void TriggerEventsByName(string aEventName)
        {
            CheckConnection();
            iClient.TriggerEventsByName(aEventName);
        }


        /// <summary>
        /// From ICallback
        /// Not used I believe.
        /// </summary>
        public void OnConnected()
        {
            //Debug.Assert(Thread.CurrentThread.IsThreadPoolThread);
            //Trace.WriteLine("Callback thread = " + Thread.CurrentThread.ManagedThreadId);

            //MessageBox.Show("OnConnected()", "Client");
        }

        /// <summary>
        /// From ICallback
        /// </summary>
        public void OnCloseOrder()
        {
            //Debug.Assert(Thread.CurrentThread.IsThreadPoolThread);
            //Trace.WriteLine("Callback thread = " + Thread.CurrentThread.ManagedThreadId);

            //MessageBox.Show("OnServerClosing()", "Client");
            //MainForm.CloseConnectionThreadSafe();
            //MainForm.CloseThreadSafe();

            if (IsReady())
            {
                string sessionId = iClient.SessionId;
                //Trace.TraceInformation("Closing client: " + sessionId);
                Close();
                //Trace.TraceInformation("Closed client: " + sessionId);
                //Fire event
                CloseOrderEvent();
            }

        }

        //From IDisposable
        public void Dispose()
        {

        }


    }


}
