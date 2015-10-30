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
    /// Define a data field on our display.
    /// Data field can be either text or bitmap.
    /// </summary>
    [DataContract]
    public class DataField
    {
        public DataField()
        {
            Index = 0;
            ColumnSpan = 1;
            RowSpan = 1;
            //Text
            Text = "";
            Alignment = ContentAlignment.MiddleLeft;
            //Bitmap
            Bitmap = null;
        }

        //Text constructor
        public DataField(int aIndex, string aText = "", ContentAlignment aAlignment = ContentAlignment.MiddleLeft)
        {
            ColumnSpan = 1;
            RowSpan = 1;
            Index = aIndex;
            Text = aText;
            Alignment = aAlignment;
            //
            Bitmap = null;
        }

        //Bitmap constructor
        public DataField(int aIndex, Bitmap aBitmap)
        {
            ColumnSpan = 1;
            RowSpan = 1;
            Index = aIndex;
            Bitmap = aBitmap;
            //Text
            Text = "";
            Alignment = ContentAlignment.MiddleLeft;
        }


        //Generic layout properties
        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public int Column { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int ColumnSpan { get; set; }

        [DataMember]
        public int RowSpan { get; set; }

        //Text properties
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public ContentAlignment Alignment { get; set; }

        //Bitmap properties
        [DataMember]
        public Bitmap Bitmap { get; set; }

        //
        public bool IsBitmap { get { return Bitmap != null; } }
        //
        public bool IsText { get { return Bitmap == null; } }
        //
        public bool IsSameLayout(DataField aField)
        {
            return (aField.ColumnSpan == ColumnSpan && aField.RowSpan == RowSpan);
        }
    }

}