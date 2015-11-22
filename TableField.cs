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
    /// Define a table data field on our display.
    /// Data field can be either text or bitmap.
    /// Each field is taking part in our display screen layout.
    /// </summary>
    [DataContract]
    public class TableField : DataField
    {
        protected TableField()
        {
            ColumnSpan = 1;
            RowSpan = 1;
        }

        [DataMember]
        public int Column { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int ColumnSpan { get; set; }

        [DataMember]
        public int RowSpan { get; set; }

        //
        public override bool IsSameLayout(DataField aField)
        {
            if (!aField.IsTableField)
            {
                return false;
            }

            TableField field = (TableField)aField;

            return (field.ColumnSpan == ColumnSpan && field.RowSpan == RowSpan);
        }
    }

}