using System;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Calling;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    class AdbCommand
    {
        private readonly FindStatus _find = new FindStatus();
        private readonly CommandExecutor _executer = new CommandExecutor();

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
                @"settings put global captive_portal_server connect.rom.miui.com/generate_204");
            var output1 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_server").Output.Out;

            return "当前设置检测服务器为：" + output1;
        }

        private string Com2(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo, @"settings put global captive_portal_use_https 1");
            _executer.AdbShell(deviceInfo,
                @"settings put global captive_portal_server connect.rom.miui.com/generate_204");
            var output1 =  _executer.AdbShell(deviceInfo, @"settings get global captive_portal_server").Output.Out;
            var output2 =  _executer.AdbShell(deviceInfo, @"settings get global captive_portal_use_https").Output.Out;

            return "当前设置检测服务器为：" + output1 + "\r\n"
                   + "HTTPS状态为：" + _find.Status(output2);
        }

        private string Com3(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo, @"settings put global captive_portal_use_https 1");
            _executer.AdbShell(deviceInfo,
                @"settings put global captive_portal_http_url http://connect.rom.miui.com/generate_204");
            _executer.AdbShell(deviceInfo,
                @"settings put global captive_portal_https_url  https://connect.rom.miui.com/generate_204");
            var output1 =  _executer.AdbShell(deviceInfo, @"settings get global captive_portal_server").Output.Out;
            var output2 =  _executer.AdbShell(deviceInfo, @"settings get global captive_portal_use_https").Output.Out;
            var output3 =  _executer.AdbShell(deviceInfo, @"settings get global captive_portal_http_url").Output.Out;
            var output4 =  _executer.AdbShell(deviceInfo, @"settings get global captive_portal_https_url").Output.Out;


            return "当前设置检测服务器状态为：" + _find.Status(output1) + "\r\n"
                   + "HTTPS状态为：" + _find.Status(output2) + "\r\n"
                   + "HTTP_URL为：" + output3 + "\r\n"
                   + "HTTPS_URL为：" + output4;
        }

        private string Com4(IDevice deviceInfo)
        {
            _executer.AdbShell(deviceInfo, @"settings put global captive_portal_use_https 1");
            _executer.AdbShell(deviceInfo,
                @"settings put global captive_portal_http_url http://connect.rom.miui.com/generate_204");
            _executer.AdbShell(deviceInfo,
                @"settings put global captive_portal_https_url  https://connect.rom.miui.com/generate_204");
            var output1 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_mode").Output.Out;
            var output2 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_use_https").Output.Out;
            var output3 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_http_url").Output.Out;
            var output4 = _executer.AdbShell(deviceInfo, @"settings get global captive_portal_https_url").Output.Out;

            return "当前设置检测服务器状态为：" + _find.Status(output1) + "\r\n"
                   + "HTTPS状态为：" + _find.Status(output2) + "\r\n"
                   + "HTTP_URL为：" + output3 + "\r\n"
                   + "HTTPS_URL为：" + output4;
        }

    }
}
