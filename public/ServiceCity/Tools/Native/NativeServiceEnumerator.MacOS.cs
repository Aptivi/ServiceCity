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
using SpecProbe.Software.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using Textify.General;

namespace ServiceCity.Tools.Native
{
    internal static partial class NativeServiceEnumerator
    {
        internal static SystemService[] EnumerateServicesMacOS()
        {
            List<SystemService> services = [];

            // Check for launchctl
            string[] launchctls = PlatformHelper.GetPossiblePaths("launchctl");
            if (launchctls.Length > 0)
            {
                string launchctl = launchctls[0];

                // launchctl is found! Use launchctl to query services and their status
                string[] serviceLines = PlatformHelper.ExecuteProcessToString(launchctl, "list").SplitNewLines().Skip(1).ToArray();
                foreach (string line in serviceLines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] fields = line.Split(['\t'], StringSplitOptions.RemoveEmptyEntries);

                    // Get the field values
                    string name = fields[2];
                    string displayName = name;

                    // Generate a service instance
                    var service = new SystemService()
                    {
                        Name = name,
                        DisplayName = displayName,
                    };
                    services.Add(service);
                }
            }

            return [.. services];
        }
    }
}
