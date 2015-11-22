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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

using System.ServiceModel;
using System.ServiceModel.Channels;


namespace SharpLib.Display
{
    /// <summary>
    /// Client side implementation of our Sharp Display Service.
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientSession : DuplexClientBase<IService>
    {
        public string SessionId { get { return InnerChannel.SessionId; } }

        public ClientSession(ICallback aCallback, string aEndpointAddress)
            : base(new InstanceContext(aCallback), new NetTcpBinding(SecurityMode.None, true), new EndpointAddress(aEndpointAddress))
        { }

        public void SetName(string aClientName)
        {
            Channel.SetName(aClientName);
        }

        public void SetLayout(TableLayout aLayout)
        {
            Channel.SetLayout(aLayout);
        }

        public void SetField(DataField aField)
        {
            Channel.SetField(aField);
        }

        public void SetFields(System.Collections.Generic.IList<DataField> aFields)
        {
            Channel.SetFields(aFields);
        }

        public int ClientCount()
        {
            return Channel.ClientCount();
        }

        public bool IsReady()
        {
            return State == CommunicationState.Opened || State == CommunicationState.Created;
        }
    }

    


}
