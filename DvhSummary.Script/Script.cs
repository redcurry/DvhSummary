using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DvhSummary.Script;
using VMS.TPS.Common.Model.API;

namespace VMS.TPS
{
    public class Script
    {
        public void Execute(ScriptContext scriptContext, Window mainWindow)
        {
            Run(scriptContext.CurrentUser,
                scriptContext.Patient,
                scriptContext.Image,
                scriptContext.StructureSet,
                scriptContext.PlanSetup,
                scriptContext.PlansInScope,
                scriptContext.PlanSumsInScope,
                mainWindow);
        }

        public void Run(
            User user,
            Patient patient,
            Image image,
            StructureSet structureSet,
            PlanSetup planSetup,
            IEnumerable<PlanSetup> planSetupsInScope,
            IEnumerable<PlanSum> planSumsInScope,
            Window mainWindow)
        {
            var mainViewModel = new MainViewModel();
            mainViewModel.PlanSetup = planSetup;

            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                mainViewModel.CalculateDvhSummary();
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }

            var mainView = new MainView();
            mainView.ViewModel = mainViewModel;

            mainWindow.Content = mainView;
        }
    }
}
