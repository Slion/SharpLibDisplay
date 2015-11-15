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
    /// Each field is taking part in our display screen layout.
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

        /// <summary>
        /// Text field constructor.
        /// </summary>
        /// <param name="aIndex">Field index, used to uniquely identify each field in our layout.</param>
        /// <param name="aText">Text content.</param>
        /// <param name="aAlignment">Text content alignment.</param>
        public DataField(int aIndex, string aText = "", ContentAlignment aAlignment = ContentAlignment.MiddleLeft)
        {
            ColumnSpan = 1;
            RowSpan = 1;
            Index = aIndex;
            Text = aText;
            Alignment = aAlignment;
            //No bitmap then
            Bitmap = null;
        }

        /// <summary>
        /// Bitmap field constructor.
        /// </summary>
        /// <param name="aIndex">Field index, used to uniquely identify each field in our layout.</param>
        /// <param name="aBitmap">Bitmap content.</param>
        public DataField(int aIndex, Bitmap aBitmap)
        {
            ColumnSpan = 1;
            RowSpan = 1;
            Index = aIndex;
            Bitmap = aBitmap;
            //No text then
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