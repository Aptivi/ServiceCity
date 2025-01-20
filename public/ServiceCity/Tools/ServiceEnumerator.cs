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

using ServiceCity.Instances;
using ServiceCity.Tools.Native;
using SpecProbe.Software.Platform;

namespace ServiceCity.Tools
{
    /// <summary>
    /// Enumeration tools for operating system services
    /// </summary>
    public static class ServiceEnumerator
    {
        /// <summary>
        /// Enumerates all installed services on your computer
        /// </summary>
        /// <returns>System service instances</returns>
        public static SystemService[] EnumerateServices()
        {
            var platform = PlatformHelper.GetPlatform();
            return platform switch
            {
                Platform.Windows => NativeServiceEnumerator.EnumerateServicesWindows(),
                Platform.MacOS => NativeServiceEnumerator.EnumerateServicesMacOS(),
                Platform.Linux => NativeServiceEnumerator.EnumerateServicesLinux(),
                _ => throw new NativeServiceException("Can't enumerate services on an unknown platform."),
            };
        }
    }
}
