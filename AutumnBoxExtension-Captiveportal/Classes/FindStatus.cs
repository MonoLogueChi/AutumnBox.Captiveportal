using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    class FindStatus
    {
        public string Status(string n)
        {
            if (n.Contains("1") || n.Contains("null")) return "开启";
            if (n.Contains("0")) return "关闭";
            return "未知状态";
        }
    }
}
