using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace LazerMarka
{
    public class IniFile
    {
        public string Path { get; }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder returnValue, int size, string filePath);

        public IniFile(string path)
        {
            Path = path;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        public string Read(string section, string key)
        {
            var returnValue = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", returnValue, 255, Path);
            return returnValue.ToString();
        }
    }
}