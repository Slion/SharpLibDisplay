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
    //All concrete types need to be listed as known type for serialization to function.
    [KnownType(typeof(TextField))]
    [KnownType(typeof(BitmapField))]
    [KnownType(typeof(RecordingField))]
    [KnownType(typeof(AudioVisualizerField))]
    [KnownType(typeof(ClientField))]    
    public abstract class DataField
    {
        protected DataField()
        {

        }

        public bool IsTextField { get { return this is TextField; } }
        public bool IsBitmapField { get { return this is BitmapField; } }
        public bool IsAudioVisualizerField { get { return this is AudioVisualizerField; } }
        public bool IsRecordingField { get { return this is RecordingField; } }
        public bool IsTableField { get { return this is TableField; } }
        public bool IsStatusField { get { return this is StatusField; } }
        public bool IsClientField { get { return this is ClientField; } }

        public abstract bool IsSameLayout(DataField aField);
        public abstract bool IsSameAs(DataField aField);

    }

}