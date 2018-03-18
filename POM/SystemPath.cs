using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POM
{
    class SystemPath
    {
        public SystemPath()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public static string GetPath()
        {
            string path;
            System.IO.FileInfo fi = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);

            path = fi.FullName.Substring(0, fi.FullName.Length - fi.Name.Length);

            fi = null;
            //path=@"F:\ChargeSystem\ChargeSystem\bin\Debug\";
            return path;
        }
    }
}
