using System;
using System.Diagnostics;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Extension;
using AutumnBoxExtension_Captiveportal.Classes;

namespace AutumnBoxExtension_Captiveportal
{
    [ExtName(name: "一键去除Wi-FI x和!号模块")]
    [ExtDesc(desc: "可以一键去除Wi-FI x和!号，该模块目前处于测试状态，不保证100%可用")]
    [ExtAuth(auth: "MonoLogueChi")]
    [ExtVersion(0, minor: 0, build: 9)]
    [ExtRequiredDeviceStates((DeviceState)2)]   //开机状态使用
    [ExtMinApi(value: 8)]
    [ExtTargetApi(value: 8)]
    //英文介绍
    [ExtName(name: "一键去除Wi-FI x和!号模块-暂定", Lang = "en-us")]
    [ExtDesc(desc: "Could 一键去除Wi-FI x和!号，该模块目前处于测试状态，Can't 保证100%可用", Lang = "en-us")]
    public class Captiveportal : AutumnBoxExtension
    {
        public override int Main()
        {
            var devBasicInfo = TargetDevice;
            var androidVersion = new DeviceBuildPropGetter(devBasicInfo).GetAndroidVersion();
            var newExt = new NewExt();
            try
            {
                if (!newExt.IsLastVersion())
                {

                    var ynGetNew = App.ShowChoiceBox("新版本", msg: $"检测到新版本，是否立即下载更新 \r\n  新版本更新日期：{NewExt.CConfig.date}", btnLeft: "否,继续执行", btnRight: "是，马上更新");

                    switch (ynGetNew)
                    {
                        case ChoiceBoxResult.Right:
                            Process.Start(NewExt.CConfig.url);
                            return 0;
                        case ChoiceBoxResult.Cancel:
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
            App.ShowLoadingWindow();
            try
            {
                st1 = new AdbCommand().V2N(androidVersion, devBasicInfo);
            }
            catch (Exception)
            {
                Logger.Warn(msg: "执行ADB命令错误");
            }
            App.CloseLoadingWindow();

            App.RunOnUIThread(() =>
            {

                var ynReboot = App.ShowChoiceBox("结束", msg: st1 + "\r\n 是否重启测试一下结果",
                    btnLeft: "再等等", btnRight: "现在重启");
                if (ynReboot == ChoiceBoxResult.Right)
                {
                    DeviceRebooter.Reboot(devBasicInfo, option: 0);
                }
            });

            return 0;
        }
    }

}
