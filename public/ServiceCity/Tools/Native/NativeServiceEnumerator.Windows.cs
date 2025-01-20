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
using System.Collections.Generic;
using System.ServiceProcess;

namespace ServiceCity.Tools.Native
{
    internal static partial class NativeServiceEnumerator
    {
        internal static SystemService[] EnumerateServicesWindows()
        {
            // Get the Windows services
            var winServices = ServiceController.GetServices();

            // Now, neutralize the service instances
            List<SystemService> services = [];
            foreach (var winService in winServices)
            {
                var name = winService.ServiceName;
                var displayName = winService.DisplayName;
                var service = new SystemService()
                {
                    Name = name,
                    DisplayName = displayName,
                };
                services.Add(service);
            }
            return [.. services];
        }
    }
}
