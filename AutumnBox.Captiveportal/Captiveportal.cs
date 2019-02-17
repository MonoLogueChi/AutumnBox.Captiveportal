using System;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Captiveportal
{
    [ExtName("一键去除Wi-FI x和!号模块", "en-us:一键去除Wi-FI x和!号模块-暂定")]
    [ExtDesc("可以一键去除Wi-FI x和!号，该模块目前处于测试状态，不保证100%可用", "en-us:Could 一键去除Wi-FI x和!号，该模块目前处于测试状态，Can't 保证100%可用")]
    [ExtAuth("MonoLogueChi")]
    [ExtVersion(0, minor: 0, build: 15)]
    [ExtRequiredDeviceStates((DeviceState)2)]   //开机状态使用
    [ExtMinApi(value: 8)]
    [ExtTargetApi(value: 8)]
    [ExtIcon(@"Resources.icon.png")]
    public class Captiveportal : LeafExtensionBase
    {
        public void Main(ILeafUI ui, ILogger logger, IDevice devices)
        {

            //启动时检测更新
            /*
            if (!NewExt.IsLastVersion)
            {
                var ynGetNew = ui.DoChoice($"检测到新版本，是否立即下载更新 \r\n  新版本更新日期：{NewExt.CConfig.date}",
                     "是，马上更新", "否,继续执行");

                switch (ynGetNew)
                {
                    case true:
                        Process.Start(NewExt.CConfig.url);
                        return;
                    case false:
                        return;
                }
            }
            */


            //获取安卓版本
            var androidVersion = new DeviceBuildPropGetter(devices).GetAndroidVersion();

            //消除X号
            string st1 = null;

            using (ui)
            {
                ui.Title = "正在设置";
                ui.Icon = this.GetIconBytes();

                ui.ShowMessage($"注意，此版本暂时删除了检查更新功能，最新版本请自行到官网查看 \r\n 某些旧版本可能会存在意想不到的问题，建议使用最新版本 \r\n 当前版本：{new VS().GetThisVersion()}");

                ui.Show();
                ui.Progress = 10;
                ui.WriteLine("正在检测安卓版本");
                ui.WriteOutput(androidVersion.ToString());
                ui.WriteLine("正在应用设置");
                st1 = new AdbCommand().V2N(androidVersion, devices).Replace("\r\n\r\n","\r\n");
                ui.WriteOutput(st1);
                ui.Progress = 80;
                var ynReboot = ui.DoChoice(st1 + "\r\n 是否重启测试一下结果",
                    "现在重启", "再等等");
                if (ynReboot == true)
                {
                    devices.Reboot2System();
                }
                ui.Title = "设置完成";
                ui.Progress = 100;
                ui.Finish();
                return;
            }
        }
    }
}
