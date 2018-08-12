using System;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Executer;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    class AdbCommand
    {
        private readonly FindStatus _find = new FindStatus();
        private readonly CommandExecuter executer = new CommandExecuter();

        public string V2N(Version version, DeviceBasicInfo deviceInfo)
        {
            if (version < Version.Parse("5.0.0")) return "低安卓版本无需去除叹号";
            if (version < Version.Parse("7.0.0")) return Com1(deviceInfo);
            if (version < Version.Parse("7.1.1")) return Com2(deviceInfo);
            if (version < Version.Parse("7.1.2")) return Com3(deviceInfo);
            if (version < Version.Parse("100.0.0")) return Com4(deviceInfo);
            return "未知版本";
        }

        private string Com1(DeviceBasicInfo deviceInfo)
        {
            var command1 = Command.MakeForAdb(deviceInfo,
                @"shell settings put global captive_portal_server connect.rom.miui.com/generate_204");
            var command2 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_server");
            executer.Execute(command1);
            var output1 = executer.Execute(command2);

            return "当前设置检测服务器为：" + output1;
        }

        private string Com2(DeviceBasicInfo deviceInfo)
        {
            var command0 = Command.MakeForAdb(deviceInfo, @"shell settings put global captive_portal_use_https 1");
            var command1 = Command.MakeForAdb(deviceInfo,
                @"shell settings put global captive_portal_server connect.rom.miui.com/generate_204");
            var command2 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_server");
            var command3 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_use_https");

            executer.Execute(command0);
            executer.Execute(command1);
            var output1 = executer.Execute(command2);
            var output2 = executer.Execute(command3);


            return "当前设置检测服务器为：" + output1 + "\r\n"
                   + "HTTPS状态为：" + _find.Status(output2.ToString());
        }

        private string Com3(DeviceBasicInfo deviceInfo)
        {
            var command0 = Command.MakeForAdb(deviceInfo, @"shell settings put global captive_portal_use_https 1");
            var command1 = Command.MakeForAdb(deviceInfo,
                @"shell settings put global captive_portal_http_url http://connect.rom.miui.com/generate_204");
            var command2 = Command.MakeForAdb(deviceInfo,
                @"shell settings put global captive_portal_https_url  https://connect.rom.miui.com/generate_204");
            var command3 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_server");
            var command4 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_use_https");
            var command5 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_http_url");
            var command6 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_https_url");

            executer.Execute(command0);
            executer.Execute(command1);
            executer.Execute(command2);
            var output1 = executer.Execute(command3);
            var output2 = executer.Execute(command4);
            var output3 = executer.Execute(command5);
            var output4 = executer.Execute(command6);

            return "当前设置检测服务器状态为：" + _find.Status(output1.ToString()) + "\r\n"
                   + "HTTPS状态为：" + _find.Status(output2.ToString()) + "\r\n"
                   + "HTTP_URL为：" + output3 + "\r\n"
                   + "HTTPS_URL为：" + output4;
        }

        private string Com4(DeviceBasicInfo deviceInfo)
        {
            var command0 = Command.MakeForAdb(deviceInfo, @"shell settings put global captive_portal_use_https 1");
            var command1 = Command.MakeForAdb(deviceInfo,
                @"shell settings put global captive_portal_http_url http://connect.rom.miui.com/generate_204");
            var command2 = Command.MakeForAdb(deviceInfo,
                @"shell settings put global captive_portal_https_url  https://connect.rom.miui.com/generate_204");
            var command3 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_mode");
            var command4 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_use_https");
            var command5 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_http_url");
            var command6 = Command.MakeForAdb(deviceInfo, @"shell settings get global captive_portal_https_url");

            executer.Execute(command0);
            executer.Execute(command1);
            executer.Execute(command2);
            var output1 = executer.Execute(command3);
            var output2 = executer.Execute(command4);
            var output3 = executer.Execute(command5);
            var output4 = executer.Execute(command6);

            return "当前设置检测服务器状态为：" + _find.Status(output1.ToString()) + "\r\n"
                   + "HTTPS状态为：" + _find.Status(output2.ToString()) + "\r\n"
                   + "HTTP_URL为：" + output3 + "\r\n"
                   + "HTTPS_URL为：" + output4;
        }

    }
}
