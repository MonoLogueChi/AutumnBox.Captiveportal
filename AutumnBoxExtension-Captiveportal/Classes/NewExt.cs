using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Open;
using Newtonsoft.Json;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    public class NewExt : ExtensionLibrarin
    {
        public override string Name { get; } = "Captiveportal";
        public override int MinApiLevel { get; } = 8;
        public override int TargetApiLevel { get; } = 8;

        
        public override void Ready()
        {
            Task.Run(() =>
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://gitee.com/monologuechi/AutumnBoxExtension-Captiveportal/raw/master/AutumnBoxExtension-Captiveportal/Captiveportal-info.json");
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8, true))
                {
                    string configStr = reader.ReadToEnd();
                    reader.Close();
                    Logger.Info(configStr);
                    //return JsonConvert.DeserializeObject<CConfig>(configStr);
                }

            });
        }
    }





    public class CConfig
    {
        public Version version { get; set; }
        public string date { get; set; }
        public string url { get; set; }
    }
}
