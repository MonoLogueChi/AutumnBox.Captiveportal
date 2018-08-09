using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Extension;
using Newtonsoft.Json;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    public class NewExt : ExtensionLibrarin
    {
        public override string Name { get; } = "Captiveportal";
        public override int MinApiLevel { get; } = 8;
        public override int TargetApiLevel { get; } = 8;

        public static CConfig cConfig = new CConfig();

        public override void Ready()
        {
            Task.Run(() =>
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://gitee.com/monologuechi/AutumnBoxExtension-Captiveportal/raw/master/AutumnBoxExtension-Captiveportal/Captiveportal-info.json");
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true))
                    {
                        string configStr = reader.ReadToEnd();
                        reader.Close();
                        cConfig = JsonConvert.DeserializeObject<CConfig>(configStr);
                    }
                }
                catch (Exception e)
                {
                    Logger.Info("检查更新失败，请检查网络连接");
                }
            });
        }
    }





    public class CConfig
    {
        public ExtVersionAttribute version { get; set; }
        public string date { get; set; }
        public string url { get; set; }
    }
}
