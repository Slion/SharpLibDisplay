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
    /// Display callback provides a means for a server to notify its clients.
    /// </summary>
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void OnConnected();

        /// <summary>
        /// Tell our client to close its connection.
        /// Notably sent when the server is shutting down.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void OnCloseOrder();
    }

}
