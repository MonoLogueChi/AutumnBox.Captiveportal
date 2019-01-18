using System;
using System.Diagnostics;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBoxExtension_Captiveportal.Classes;

namespace AutumnBoxExtension_Captiveportal
{
    [ExtName("一键去除Wi-FI x和!号模块", "en-us:一键去除Wi-FI x和!号模块-暂定")]
    [ExtDesc("可以一键去除Wi-FI x和!号，该模块目前处于测试状态，不保证100%可用", "en-us:Could 一键去除Wi-FI x和!号，该模块目前处于测试状态，Can't 保证100%可用")]
    [ExtAuth("MonoLogueChi")]
    [ExtVersion(0, minor: 0, build: 14)]
    [ExtRequiredDeviceStates((DeviceState)2)]   //开机状态使用
    [ExtMinApi(value: 8)]
    [ExtTargetApi(value: 8)]
    [ExtIcon(@"Resources.icon.png")]
    public class Captiveportal : LeafExtensionBase
    {
        public int Main(IUx ux, ILogger Logger, IDevice DeviceNow)
        {
            var devBasicInfo = DeviceNow;
            var androidVersion = new DeviceBuildPropGetter(devBasicInfo).GetAndroidVersion();
            try
            {
                if (!NewExt.IsLastVersion)
                {
                    var ynGetNew = ux.DoChoice($"检测到新版本，是否立即下载更新 \r\n  新版本更新日期：{NewExt.CConfig.date}",
                        btnLeft: "否,继续执行", btnRight: "是，马上更新");
                    switch (ynGetNew)
                    {
                        case ChoiceResult.Right:
                            Process.Start(NewExt.CConfig.url);
                            return 0;
                        case ChoiceResult.Cancel:
                            return 0;
                    }

                }
            }
            catch (Exception)
            {
                Logger.Info(msg: "检测更新失败，未知错误");
            }

            //这些是去除X号操作
            string st1 = null;
            ux.ShowLoadingWindow();
            try
            {
                st1 = new AdbCommand().V2N(androidVersion, devBasicInfo);
            }
            catch (Exception)
            {
                Logger.Warn(msg: "执行ADB命令错误");
            }
            ux.CloseLoadingWindow();
            ux.RunOnUIThread(() =>
            {
                var ynReboot = ux.DoChoice(message: st1 + "\r\n 是否重启测试一下结果",
                    btnLeft: "再等等", btnRight: "现在重启");
                if (ynReboot == ChoiceResult.Right)
                {
                    devBasicInfo.Reboot2System();
                }
            });
            return 0;
        }
    }

}
