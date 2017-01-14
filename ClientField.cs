﻿// Copyright (C) 2014-2015 Stéphane Lenclud.
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
    /// Define a field for our display layout representing our client layout.
    /// TODO: Add name property to be able to include foreign client field.
    /// </summary>
    [DataContract]
    public class ClientField : TableField
    {
        public ClientField()
        {
        }

        /// <summary>
        /// Client field constructor.
        /// </summary>
        /// <param name="aIndex">Field index, used to uniquely identify each field in our layout.</param>
        /// <param name="aBitmap">Bitmap content.</param>
        public ClientField(int aColumn = 0, int aRow = 0, int aColumnSpan = 1, int aRowSpan = 1)
            : base(aColumn, aRow, aColumnSpan, aRowSpan)
        {

        }

    }

}