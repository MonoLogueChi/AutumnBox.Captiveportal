using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Extension;
using AutumnBoxExtension_Captiveportal.Classes;

namespace AutumnBoxExtension_Captiveportal
{
    [ExtName("一键去除Wi-FI x和!号模块")]
    [ExtDesc("可以一键去除Wi-FI x和!号，该模块目前处于测试状态，不保证100%可用")]
    [ExtAuth("MonoLogueChi")]
    [ExtVersion(0, 0, 9)]
    [ExtRequiredDeviceStates((DeviceState)2)]   //开机状态使用
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    //英文介绍
    [ExtName("一键去除Wi-FI x和!号模块-暂定", Lang = "en-us")]
    [ExtDesc("Could 一键去除Wi-FI x和!号，该模块目前处于测试状态，Can't 保证100%可用", Lang = "en-us")]
    public class Captiveportal : AutumnBoxExtension
    {
        public override int Main()
        {
            var devBasicInfo = TargetDevice;
            var androidVersion = new DeviceBuildPropGetter(devBasicInfo).GetAndroidVersion();
            string st1 = null;

            App.ShowLoadingWindow();
            try
            {
                st1 = new AdbCommand().V2N(androidVersion, devBasicInfo);
            }
            catch (Exception e)
            {
                Logger.Warn("执行ADB命令错误");
            }
            App.CloseLoadingWindow();

            App.RunOnUIThread(() =>
            {
                
                var ynReboot = App.ShowChoiceBox("结束", st1 + "\r\n 是否重启测试一下结果",
                    "再等等", "现在重启");
                if (ynReboot == ChoiceBoxResult.Right)
                {
                    DeviceRebooter.Reboot(devBasicInfo, option: (RebootOptions)0);
                }
            });

            return 0;
        }
    }

}
