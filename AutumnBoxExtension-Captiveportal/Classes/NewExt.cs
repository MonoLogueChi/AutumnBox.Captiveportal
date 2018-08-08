using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Open;

namespace AutumnBoxExtension_Captiveportal.Classes
{
    public class NewExt : ExtensionLibrarin
    {
        public override string Name { get; } = "Captiveportal";
        public override int MinApiLevel { get; } = 8;
        public override int TargetApiLevel { get; } = 8;

        
        public override void Ready()
        {
            Logger.Info("启动时要调用更新");
        }
    }


}
