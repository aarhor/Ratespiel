using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spielsücht.Klassen
{
    public static class Windows_Version
    {
        public static string GetWindwosClientVersion()
        {
            int major = Environment.OSVersion.Version.Major;
            int minor = Environment.OSVersion.Version.Minor;
            int build = Environment.OSVersion.Version.Build;


            if (major == 4 && minor == 0 && build == 950)
                return "Windows 95 Release 1";
            else if (major == 4 && minor == 0 && build == 1111)
                return "Windows 95 Release 2";
            else if (major == 4 && minor == 3 && (build == 1212 || build == 1213 || build == 1214))
                return "Windows 95 Release 2.1";
            else if (major == 4 && minor == 10 && build == 1998)
                return "Windows 98";
            else if (major == 4 && minor == 10 && build == 2222)
                return "Windows 98 Second Edition";
            else if (major == 4 && minor == 90)
                return "Windows Me";
            else if (major == 5 && minor == 0)
                return "Windows 2000";
            else if (major == 5 && minor == 1 && build == 2600)
                return "Windows XP";
            else if (major == 6 && minor == 0)
                return "Windows Vista";
            else if (major == 6 && minor == 1)
                return "Windows 7";
            else if (major == 6 && minor == 2 && build == 9200)
                return "Windows 8 | Windows 8.1";
            else if (major == 6 && minor == 2 && build == 9600)
                return "Windows 8.1 Update 1";
            else if (major == 10 && minor == 0 && build == 10240)
                return "Windows 10";
            else
                return "Die Windows Version konnte nicht ermittelt werden.";
        }
    }
}
