//
// ServiceCity  Copyright (C) 2024-2025  Aptivi
//
// This file is part of ServiceCity
//
// ServiceCity is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// ServiceCity is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ServiceCity.Instances
{
    /// <summary>
    /// Thrown when native service exception occurs
    /// </summary>
    public class NativeServiceException : Exception
    {
        /// <summary>
        /// Creates a new service exception class
        /// </summary>
        public NativeServiceException()
        { }

        /// <summary>
        /// Creates a new service exception class
        /// </summary>
        /// <param name="message">Exception message to include</param>
        public NativeServiceException(string message) :
            base(message)
        { }

        /// <summary>
        /// Creates a new service exception class
        /// </summary>
        /// <param name="message">Exception message to include</param>
        /// <param name="innerException">Inner exception to include</param>
        public NativeServiceException(string message, Exception innerException) :
            base(message, innerException)
        { }
    }
}
