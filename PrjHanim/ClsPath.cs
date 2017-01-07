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
            string clPathBase = ClsPath.GetPath("");
            return (clPathBase);
        }

        public static string GetPath(string clPath)
        {
            Assembly clAssembly = Assembly.GetExecutingAssembly();
            string clPathBase = Path.GetDirectoryName(clAssembly.Location);
            clPathBase = Path.Combine(clPathBase, clPath);
            return (clPathBase);
        }
    }
}
