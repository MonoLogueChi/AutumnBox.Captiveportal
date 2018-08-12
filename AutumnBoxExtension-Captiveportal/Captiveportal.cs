using System;
using System.Diagnostics;
using AutumnBox.Basic.Device;
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
        /// <summary>
        /// 模块版本号，暂时未找到获取自身版本号接口
        /// </summary>
        private Version thisVersion = Version.Parse("0.0.9");


        public override int Main()
        {
            var devBasicInfo = TargetDevice;
            var androidVersion = new DeviceBuildPropGetter(devBasicInfo).GetAndroidVersion();
            var tmpPath = Tmp.Path;

            try
            {
                if (thisVersion < NewExt.cConfig.version)
                {

                    var ynGetNew = App.ShowChoiceBox("新版本", "检测到新版本，是否立即下载更新 \r\n  新版本更新日期：" + NewExt.cConfig.date, "否,继续执行", "是，马上更新");

                    if (ynGetNew == ChoiceBoxResult.Right)
                    {
                        Process.Start(NewExt.cConfig.url);
                        return 0;
                    }
                    else if (ynGetNew == ChoiceBoxResult.Cancel)
                    {
                        return 0;
                    }

                }
            }
            catch (Exception)
            {
                Logger.Info("检测更新失败，未知错误");
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
