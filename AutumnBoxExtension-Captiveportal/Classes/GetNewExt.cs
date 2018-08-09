using System.IO;
using System.Net;
using System.Diagnostics;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    public class GetNewExt
    {
        public void DownLoadFiles(string url, string path)
        {
            var NewCaptiveportal = "https://dl.sm9.top/AutumuBox/Extension/Captiveportal/bat/NewCaptiveportal.bat";

            WebClient client = new WebClient();
            client.DownloadFile(NewCaptiveportal, Path.Combine(path, "NewCaptiveportal.bat"));
            client.DownloadFile(url, Path.Combine(path, ".."));
        }

        public void StartCmd(string path)
        {
            Process process = new Process();

            // 初始化可执行文件的一些基础信息
            process.StartInfo.WorkingDirectory = path; // 初始化可执行文件的文件夹信息
            process.StartInfo.FileName = "NewCaptiveportal.bat"; // 初始化可执行文件名

            process.StartInfo.UseShellExecute = true;        // 使用操作系统shell启动进程

            // 启动可执行文件
            process.Start();
        }



    }
}
