using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Wrapper;
using Newtonsoft.Json;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    public class NewExt : ExtensionLibrarin
    {
        public override string Name { get; } = "Captiveportal";
        public override int MinApiLevel { get; } = 8;
        public override int TargetApiLevel { get; } = 8;
        public static CConfig CConfig { get; set; } = new CConfig();

        public override void Ready()
        {
            Task.Run(() =>
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/MonoLogueChi/AutumnBoxExtension-Captiveportal/master/AutumnBoxExtension-Captiveportal/Captiveportal-info.json");
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream: stream, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: true))
                    {
                        string configStr = reader.ReadToEnd();
                        reader.Close();
                        CConfig = JsonConvert.DeserializeObject<CConfig>(configStr);
                    }
                }
                catch (Exception)
                {
                    Logger.Info("检查更新失败，请检查网络连接");
                }
            });
        }

        public bool IsLastVersion()
        {
            ClassExtensionScanner CType = new ClassExtensionScanner(typeof(Captiveportal));

            CType.Scan((ClassExtensionScanner.ScanOption) 1);
            var oldVersion = CType.Informations["VERSION"].Value as Version;

            //Logger.Info(oldVersion.ToString());
            return oldVersion >= CConfig.version;

        }
    }

    public class CConfig
    {
        public Version version { get; set; }
        public string date { get; set; }
        public string url { get; set; }
    }

}
