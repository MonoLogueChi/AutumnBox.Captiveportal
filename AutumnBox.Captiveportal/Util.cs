﻿using System;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;

namespace AutumnBox.Captiveportal
{
    /// <summary>
    /// adb命令
    /// </summary>
    public class AdbCommand
    {
        private readonly CommandExecutor _executer = new CommandExecutor();
        const string _url = "connect.rom.miui.com/generate_204";

        public string V2N(Version version, IDevice deviceInfo)
        {
            if (version < Version.Parse("5.0.0")) return "低安卓版本无需去除叹号";
            if (version < Version.Parse("7.0.0")) return Com1(deviceInfo);
            if (version < Version.Parse("7.1.1")) return Com2(deviceInfo);
            if (version < Version.Parse("7.1.2")) return Com3(deviceInfo);
            if (version < Version.Parse("100.0.0")) return Com4(deviceInfo);
            return "未知版本";
        }


        private string Com1(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo,
                $"settings put global captive_portal_server {_url}");
            var output1 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_server").Output;

            return "当前设置检测服务器为：" + output1;
        }

        private string Com2(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo, @"settings put global captive_portal_use_https 1");
            _executer.AdbShell(deviceInfo,
                $"settings put global captive_portal_server {_url}");
            var output1 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_server").Output;
            var output2 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_use_https").Output;

            return "当前设置检测服务器为：" + output1 + "\r\n"
                   + "HTTPS状态为：" + Status(output2);
        }

        private string Com3(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo, @"settings put global captive_portal_use_https 1");
            _executer.AdbShell(deviceInfo,
                $"settings put global captive_portal_http_url http://{_url}");
            _executer.AdbShell(deviceInfo,
                $"settings put global captive_portal_https_url  https://{_url}");
            var output1 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_server").Output;
            var output2 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_use_https").Output;
            var output3 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_http_url").Output;
            var output4 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_https_url").Output;


            return "当前设置检测服务器状态为：" + Status(output1) + "\r\n"
                   + "HTTPS状态为：" + Status(output2) + "\r\n"
                   + "HTTP_URL为：" + output3
                   + "HTTPS_URL为：" + output4;
        }

        private string Com4(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo, @"settings put global captive_portal_use_https 1");
            _executer.AdbShell(deviceInfo,
                $"settings put global captive_portal_http_url http://{_url}");
            _executer.AdbShell(deviceInfo,
                $"settings put global captive_portal_https_url  https://{_url}");
            var output1 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_mode").Output;
            var output2 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_use_https").Output;
            var output3 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_http_url").Output;
            var output4 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_https_url").Output;

            return "当前设置检测服务器状态为：" + Status(output1) + "\r\n"
                   + "HTTPS状态为：" + Status(output2) + "\r\n"
                   + "HTTP_URL为：" + output3 + "\r\n"
                   + "HTTPS_URL为：" + output4;
        }

        private string Status(string n)
        {
            if (n.Contains("1") || n.Contains("null")) return "开启";
            if (n.Contains("0")) return "关闭";
            return "未知状态";
        }
    }
}
