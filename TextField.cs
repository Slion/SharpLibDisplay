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
    /// Define a text field on our display.
    /// Each field is taking part in our display screen layout.
    /// </summary>
    [DataContract]
    public class TextField : TableField
    {
        public TextField()
        {
            //Text
            Text = "";
            Alignment = ContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Text field constructor.
        /// </summary>
        /// <param name="aIndex">Field index, used to uniquely identify each field in our layout.</param>
        /// <param name="aText">Text content.</param>
        /// <param name="aAlignment">Text content alignment.</param>
        public TextField(int aIndex, string aText = "", ContentAlignment aAlignment = ContentAlignment.MiddleLeft)
        {
            Index = aIndex;
            Text = aText;
            Alignment = aAlignment;
        }

        //Text properties
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public ContentAlignment Alignment { get; set; }

    }

}