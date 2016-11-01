using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrjHikariwoAnim
{
    public class ClsPath
    {
        public static string GetPath()
        {
            Assembly clAssembly = Assembly.GetExecutingAssembly();
            string clPath = Path.GetDirectoryName(clAssembly.Location);
            return (clPath);
        }

        public static string GetPathRoaming(string clPath)
        {
            string clPathRoaming = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string clPathBase = Path.Combine(clPathRoaming, clPath);
            return (clPathBase);
        }
    }
}
