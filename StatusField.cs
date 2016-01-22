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

using System.Runtime.Serialization;
using System.Drawing;

namespace SharpLib.Display
{

    /// <summary>
    /// Define a status data field on our display.
    /// Status field is the base for fields not taking part in table layout.
    /// Status fields are typically unique and global.
    /// Intended for clients to activate icons on Consumer Electronic Displays.
    /// </summary>
    [DataContract]
    public class StatusField : DataField
    {
        protected StatusField()
        {
        }

        public override bool IsSameLayout(DataField aField)
        {
            //By default status field have the same layout if they have the same type
            return GetType() == aField.GetType();
        }

        public override bool IsSameAs(DataField aField)
        {
            //By default status field are the same if they have the same type
            //Typically each status field is unique so we are ok
            return GetType() == aField.GetType();
        }

    }

}