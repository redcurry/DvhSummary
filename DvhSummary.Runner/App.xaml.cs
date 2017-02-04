using System.Windows;
using EclipsePlugInRunner.Scripting;

namespace DvhSummary.Runner
{
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ScriptRunner.Run(new VMS.TPS.Script());
        }
    }
}
