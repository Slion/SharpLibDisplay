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

namespace SharpLib.Display
{

    /// <summary>
    /// Priority constants are provided as an indication of the kind of applications they are intended for        
    /// </summary>
    public static class Priorities
    {        
        public const uint Highest = uint.MaxValue;
        public const uint Foreground = 1000;
        public const uint Game = 800;
        public const uint MediaCenter = 600;
        public const uint Default = 500;
        public const uint MediaServer = 400;
        public const uint SystemMonitor = 200;
        public const uint Background = 100;
        public const uint Lowest = uint.MinValue;
    }
}