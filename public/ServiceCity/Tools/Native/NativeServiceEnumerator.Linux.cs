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
using System.IO;
using System.Linq;
using Textify.General;

namespace ServiceCity.Tools.Native
{
    internal static partial class NativeServiceEnumerator
    {
        internal static SystemService[] EnumerateServicesLinux()
        {
            List<SystemService> services = [];

            // Check for systemd
            string[] systemctls = PlatformHelper.GetPossiblePaths("systemctl");
            if (systemctls.Length > 0)
            {
                string systemctl = systemctls[0];

                // Systemd is found! Use systemctl to query services and their status
                string[] serviceUnitLines = PlatformHelper.ExecuteProcessToString(systemctl, "list-units --full --all -t service --no-legend --plain").SplitNewLines();
                foreach (string line in serviceUnitLines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    string[] fields = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);

                    // Get the field values
                    string name = fields[0];
                    string displayName = string.Join(" ", fields.Skip(4));

                    // Generate a service instance
                    var service = new SystemService()
                    {
                        Name = name,
                        DisplayName = displayName,
                    };
                    services.Add(service);
                }
            }

            // Check old-style init.d files
            string[] initdServices = Directory.GetFiles("/etc/init.d");
            foreach (string servicePath in initdServices)
            {
                string name = Path.GetFileNameWithoutExtension(servicePath);
                string displayName = "";

                // Read the name from the script
                using var scriptStream = File.OpenRead(servicePath);
                using var scriptStreamReader = new StreamReader(scriptStream);
                while (!scriptStreamReader.EndOfStream)
                {
                    string line = scriptStreamReader.ReadLine();
                    if (line == "### END INIT INFO")
                        break;

                    // Search for display name
                    if (line.StartsWithNoCase("# Short-Description:"))
                        displayName = line.RemovePrefix("# Short-Description:").Trim();
                }

                // Generate a service instance
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
