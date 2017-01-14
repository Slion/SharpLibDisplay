// Copyright (C) 2014-2015 Stéphane Lenclud.
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

using System.ServiceModel;


namespace SharpLib.Display
{
    /// <summary>
    /// Define our display service.
    /// Clients and servers must implement it to communicate with one another.
    /// Through this service clients can send requests to a server.
    /// Through this service a server session can receive requests from a client.
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ICallback), SessionMode = SessionMode.Required)]
    public interface IService
    {
        /// <summary>
        /// Set the name of this client.
        /// Name is a convenient way to recognize your client.
        /// Naming you client is not mandatory.
        /// In the absence of a name the session ID is often used instead.
        /// </summary>
        /// <param name="aClientName"></param>
        [OperationContract(IsOneWay = true)]
        void SetName(string aClientName);

        /// <summary>
        /// Define client priority.
        /// Client with higher priority should come first and take control of the display.
        /// </summary>
        /// <param name="aPriority"></param>
        [OperationContract(IsOneWay = true)]
        void SetPriority(uint aPriority);
        
        /// <summary>
        /// Define to which target following calls to SetLayout, SetField and SetFields will apply.
        /// Our default target is our Client.
        /// </summary>
        /// <param name="aPriority"></param>
        [OperationContract(IsOneWay = true)]
        void SetTarget(Target aTarget);

        /// <summary>
        /// Define table layout for our fields on our display.
        /// </summary>
        /// <param name="aLayout"></param>
        [OperationContract(IsOneWay = true)]
        void SetLayout(TableLayout aLayout);

        /// <summary>
        /// Set the given field on your display.
        /// Fields are often just lines of text or bitmaps.
        /// </summary>
        /// <param name="aTextFieldIndex"></param>
        [OperationContract(IsOneWay = true)]
        void SetField(DataField aField);

        /// <summary>
        /// Allows a client to set multiple fields at once.
        /// </summary>
        /// <param name="aFields"></param>
        [OperationContract(IsOneWay = true)]
        void SetFields(System.Collections.Generic.IList<DataField> aFields);

        /// <summary>
        /// Provides the number of clients currently connected
        /// </summary>
        /// <returns></returns>
        [OperationContract()]
        int ClientCount();

        /// <summary>
        /// Trigger server side events matching the given name.
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void TriggerEventsByName(string aEventName);


    }
}
