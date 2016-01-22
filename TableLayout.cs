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
using System.Windows.Forms;
using System.Collections.Generic;

namespace SharpLib.Display
{
    /// <summary>
    /// For client to specify a specific layout.
    /// A table layout is sent from client to server and defines data fields layout on our display.
    /// </summary>
    [DataContract]
    public class TableLayout
    {
        [DataMember]
        public List<ColumnStyle> Columns { get; set; }

        [DataMember]
        public List<RowStyle> Rows { get; set; }


        public TableLayout()
        {
            Columns = new List<ColumnStyle>();
            Rows = new List<RowStyle>();
        }

        /// <summary>
        /// Construct our table layout.
        /// </summary>
        /// <param name="aColumnCount">Number of column in our table.</param>
        /// <param name="aRowCount">Number of rows in our table.</param>
        public TableLayout(int aColumnCount, int aRowCount)
        {
            Columns = new List<ColumnStyle>();
            Rows = new List<RowStyle>();

            for (int i = 0; i < aColumnCount; i++)
            {
                Columns.Add(new ColumnStyle(SizeType.Percent, 100 / aColumnCount));
            }

            for (int i = 0; i < aRowCount; i++)
            {
                Rows.Add(new RowStyle(SizeType.Percent, 100 / aRowCount));
            }
        }

        /// <summary>
        /// Compare two TableLayout object.
        /// </summary>
        /// <returns>Tells whether both layout are identical.</returns>
        public bool IsSameAs(TableLayout aTableLayout)
        {
            //Check rows and columns counts
            if (Columns.Count != aTableLayout.Columns.Count || Rows.Count != aTableLayout.Rows.Count)
            {
                return false;
            }

            //Compare each columns
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].SizeType != aTableLayout.Columns[i].SizeType)
                {
                    return false;
                }

                if (Columns[i].Width != aTableLayout.Columns[i].Width)
                {
                    return false;
                }
            }

            //Compare each columns
            for (int i = 0; i < Rows.Count; i++)
            {
                if (Rows[i].SizeType != aTableLayout.Rows[i].SizeType)
                {
                    return false;
                }

                if (Rows[i].Height != aTableLayout.Rows[i].Height)
                {
                    return false;
                }
            }

            //Both rows and columns have the same content.
            return true;
        }

    }
}