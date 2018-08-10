using System.IO;
using System.Net;
using System.Diagnostics;
using AutumnBox.Basic.Executer;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    //临时更新方案
    public class GetNewExt
    {
        public void DownLoadFiles(string url, string path)
        {
            var newCaptiveportalBat = "https://dl.sm9.top/AutumuBox/Extension/Captiveportal/bat/NewCaptiveportal.bat";

            WebClient client = new WebClient();
            client.DownloadFile(newCaptiveportalBat, Path.Combine(path, "NewCaptiveportal.bat"));
            client.DownloadFile(url, Path.Combine(path, "AutumnBoxExtension-Captiveportal.dll"));
        }


        public void StartCmd(string path)
        {
            new CommandExecuter().Execute(Command.MakeForCmd("Start "+ Path.Combine(path, "NewCaptiveportal.bat")));
        }

    }
}
